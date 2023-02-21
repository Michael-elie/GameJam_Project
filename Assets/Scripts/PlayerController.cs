using System;
using System.Collections;
using System.Collections.Generic;
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
    public ParticleSystem firefx;
    public AudioSource firesound;
    
    public ParticleSystem destructionfx;
    public AudioSource destructionsound;

    [SerializeField] private float playerPV;
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
    private bool sausaceEnable = true;
    private bool iceCreamEnable = true;
    
    
   
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        
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
      
      





      if (Input.GetKeyDown(KeyCode.Alpha1) && donutEnable == true )
      {
          Health();
          
      }
      
      if (Input.GetKeyDown(KeyCode.Alpha2) && bananaEnable == true)
      {
          SpeedUP();
          Debug.Log("speedup");
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

    private void Move()
    {
        
        _animator.SetInteger ("AnimationPar", 1);
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horInput, 0f, verInput);
        moveDestination = transform.position + movement;
        Agent.destination = moveDestination;
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
       //firefx.Play();
       //firesound.Play();
        Instantiate(BulletPrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);
        yield return new WaitForSeconds(0.5f);
        IsAlreadyFiring = false;
    }
    
    public void ApplyDamage(float damage)
    {

        playerPV -= damage;
        if (playerPV <= 0)
        {
            Destruction();
        }

    }
    
    protected virtual void Destruction()
    {
        mainCamera.transform.SetParent(null);
       // destructionfx.transform.SetParent(null);
       //   destructionsound.Play();
       //   destructionfx.Play();
        Destroy(gameObject);
        _enemyController._animator.SetInteger("SlimAnimation",3 );
    }

    void  Health()
    {
        playerPV = playerPV + 20f;
    }
    
    void  SpeedUP()
    {
        moveSpeed = moveSpeed + 4f;
    }
    
    void Explosion()
    {
        Instantiate(BulletExplosionPrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);
        sausaceEnable = false;
    }
    
    void FreezeEnemy()
    {
        Instantiate(BulletFreezePrefab, BulletSpawnPosition.position, BulletSpawnPosition.rotation);
        iceCreamEnable = false;
    }
}
