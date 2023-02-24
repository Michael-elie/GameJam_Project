using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera; 
    
    private bool IsAlreadyFiring = false;
    public ParticleSystem healthfx;
    public ParticleSystem Speedfx;
    public AudioSource firesound;
    public AudioSource abilitysound;
    public AudioSource deathsound;
    public AudioSource explosionsound;
    public AudioSource damagesound;

    public float playerPV = 100;
    public float maxPlayerPV = 100f;
    [SerializeField] private bool boostSpeed = false;
    public float playerDamage; 
    
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject BulletExplosionPrefab;
    [SerializeField] private GameObject BulletFreezePrefab;
    [SerializeField] private Transform BulletSpawnPosition;
    [SerializeField] private Animator _animator;
    private NavMeshAgent Agent;
    private Vector3 moveDestination;
    private EnemyController _enemyController;
    public Timer timer;

    private bool donutEnable = false;
    private bool bananaEnable = false;
    private bool sausaceEnable = false;
    private bool iceCreamEnable = false;

    [SerializeField] private GameObject donutready;
    [SerializeField] private GameObject bananeready;
    [SerializeField] private GameObject saucisseready;
    [SerializeField] private GameObject icecreamready;
    
    public float donutValue = 0;
    public float bananeValue = 0;
    public float sausaceValue = 0;
    public float iceCreamValue = 0;
    
    private float donutMaxValue = 4;
    private float bananeMaxValue = 4; 
    private float sausaceMaxValue = 4;
    private float iceCreaMaxValue = 4;

   public GameUI _gameUI; 
   
    public delegate void PlayerEvents();

    public static event PlayerEvents OnUpdateHealth;

   
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        mainCamera = FindObjectOfType<Camera>();
        OnUpdateHealth?.Invoke();
    }
    void Update()
    {
        _gameUI.bananeBar.fillAmount = bananeValue / bananeMaxValue; 
        _gameUI.icecreamBar.fillAmount = iceCreamValue / iceCreaMaxValue;
        _gameUI.donutBar.fillAmount = donutValue / donutMaxValue;
        _gameUI.saucisseBar.fillAmount = sausaceValue / sausaceMaxValue;
        
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
      Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
      float RayLength;

      if (groundPlane.Raycast(cameraRay, out RayLength))
      {
          Vector3 pointToLook = cameraRay.GetPoint(RayLength);
         // Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);
          
          transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
      }
      
      if (Input.GetMouseButton(0))
      {
          Fire();
      }
      Move();
      
      
      if (donutValue == donutMaxValue)
      {
          donutEnable = true;
          donutready.SetActive(true);
      }
      else if (sausaceValue == sausaceMaxValue)
      {
          sausaceEnable = true;
          saucisseready.SetActive(true);
      }
      else if (bananeValue == bananeMaxValue)
      {
          bananaEnable = true;
          bananeready.SetActive(true);
      }
      else if (iceCreamValue == iceCreaMaxValue)
      {
          iceCreamEnable = true;
          icecreamready.SetActive(true);
      }

      if ( moveDestination == transform.position )
      {
          _animator.SetInteger("AnimationPar", 0);
      }
      
      if (boostSpeed == false)
        
      {
          gameObject.GetComponent<NavMeshAgent>().speed =  5f;
      }
      

      if (Input.GetKeyDown(KeyCode.Alpha1) && donutEnable == true )
      {
         healthfx.Play();
          Health();
      }
      
      if (Input.GetKeyDown(KeyCode.Alpha2) && bananaEnable == true)
      { 
          Speedfx.Play();
          boostSpeed = true;
          StartCoroutine(SpeedUP());
         
      }
      if (Input.GetKeyDown(KeyCode.Alpha3) && sausaceEnable == true)
      {
          Explosion();
      }
      
      if (Input.GetKeyDown(KeyCode.Alpha4) &&  iceCreamEnable == true)
      {
          FreezeEnemy();
      }
      
    }
    IEnumerator SpeedUP()
    {
        if (boostSpeed == true)
        {
            abilitysound.Play();
            gameObject.GetComponent<NavMeshAgent>().speed =   gameObject.GetComponent<NavMeshAgent>().speed + 1f;
            yield return new WaitForSeconds(3f);
            boostSpeed = false;
            bananeValue = 0;
        }
        bananaEnable = false;
        bananeready.SetActive(false);
        _gameUI.bananeBar.fillAmount = bananeValue / bananeMaxValue; 
    }
    private void Move()
    {
        _animator.SetInteger("AnimationPar", 1);
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horInput, 0f, verInput);
        moveDestination = transform.position + movement;
        Agent.SetDestination(moveDestination);
    }
    
    
    protected void Fire()
    {
        if (!IsAlreadyFiring)
        {
            IsAlreadyFiring = true;
            StartCoroutine(fireDelay());
        }
    }
    IEnumerator fireDelay()
    
    {   
       
       firesound.Play();
        Instantiate(BulletPrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);
        yield return new WaitForSeconds(0.27f);
        IsAlreadyFiring = false;
    } 
    
    public void ApplyDamage(float damage)
    {
        damagesound.Play();
        playerPV -= damage;
        OnUpdateHealth?.Invoke();
        if (playerPV <= 0)
        {
            Destruction();
        }
    }
    protected virtual void Destruction()
    {
        //mainCamera.transform.SetParent(null);
     
       //  deathsound.Play();
       //Destroy(gameObject,0f);
       SceneManager.LoadScene("GameOver");
    }

    void  Health()
    {
        abilitysound.Play();
        playerPV = playerPV + 25f;
        donutEnable = false;
        donutValue = 0;
        OnUpdateHealth?.Invoke();
        _gameUI.donutBar.fillAmount = donutValue / donutMaxValue;
        donutready.SetActive(false);
    }
    
    void Explosion()
    {
        explosionsound.Play();
        Instantiate(BulletExplosionPrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);
        sausaceEnable = false;
        sausaceValue = 0;
        saucisseready.SetActive(false);
        _gameUI.saucisseBar.fillAmount = sausaceValue / sausaceMaxValue;
    }
    void FreezeEnemy()
    {
        abilitysound.Play();
        Instantiate(BulletFreezePrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);
        iceCreamEnable = false;
        iceCreamValue = 0;
        icecreamready.SetActive(false);
        _gameUI.icecreamBar.fillAmount = iceCreamValue / iceCreaMaxValue;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("banane"))
        {
            if (bananeValue < bananeMaxValue)
            {
                bananeValue = bananeValue + 1; 
                _gameUI.bananeBar.fillAmount = bananeValue / bananeMaxValue;
            }
            

        }
        else if (other.gameObject.CompareTag("icecream"))
        {
            iceCreamValue = iceCreamValue + 1;
            _gameUI.icecreamBar.fillAmount = iceCreamValue / iceCreaMaxValue;
        }  
        else if (other.gameObject.CompareTag("donut"))
        {
            donutValue = donutValue + 1;
            _gameUI.donutBar.fillAmount = donutValue / donutMaxValue;
        }  
        else if (other.gameObject.CompareTag("saucisse"))
        {
            sausaceValue = sausaceValue + 1;
            _gameUI.saucisseBar.fillAmount = sausaceValue / sausaceMaxValue;
        }  
    }
}
