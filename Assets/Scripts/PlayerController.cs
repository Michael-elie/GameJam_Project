using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

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

    public float playerPV;
    public float maxPlayerPV = 100f;
    public float moveSpeed = 2.5f;
    public float playerDamage; 
    
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject BulletExplosionPrefab;
    [SerializeField] private GameObject BulletFreezePrefab;
    [SerializeField] private Transform BulletSpawnPosition;
    [SerializeField] private Animator _animator;
    private NavMeshAgent Agent;
    private Vector3 moveDestination;
    private EnemyController _enemyController;

    private bool donutEnable = false;
    private bool bananaEnable = false;
    private bool sausaceEnable = false;
    private bool iceCreamEnable = true;
    [SerializeField] private bool boostSpeed = false;

    
    public delegate void PlayerEvents();

    public static event PlayerEvents OnUpdateHealth;

   
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        OnUpdateHealth?.Invoke();
    }
    void Update()
    {
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

      if ( moveDestination == transform.position )
      {
          _animator.SetInteger("AnimationPar", 0);
      }
      
      if (boostSpeed == false)
        
      {
          gameObject.GetComponent<NavMeshAgent>().speed =  4f;
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
        }
        bananaEnable = false;
    }

    private void Move()
    {
        
        _animator.SetInteger("AnimationPar", 1);
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horInput, 0f, verInput);
        moveDestination = transform.position + movement;
        Debug.Log(moveDestination);
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
        yield return new WaitForSeconds(0.25f);
        IsAlreadyFiring = false;
    }
    
    public void ApplyDamage(float damage)
    {
        damagesound.Play();
        playerPV -= damage;
        if (playerPV <= 0)
        { 
          
          //  _animator.SetInteger("AnimationPar", 2);
            Destruction();
            _enemyController._animator.SetInteger("SlimAnimation",3);
            
        }
        OnUpdateHealth?.Invoke();

    }
    
    protected virtual void Destruction()
    {
        mainCamera.transform.SetParent(null);
     
       //  deathsound.Play();
       Destroy(gameObject,0f);
    }

    void  Health()
    {
        abilitysound.Play();
        playerPV = playerPV + 20f;
        donutEnable = false;
        OnUpdateHealth?.Invoke();
    }
    
    void Explosion()
    {
        explosionsound.Play();
        Instantiate(BulletExplosionPrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);
        sausaceEnable = false;
    }
    
    void FreezeEnemy()
    {
        abilitysound.Play();
        Instantiate(BulletFreezePrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);
        iceCreamEnable = false;
    }
}
