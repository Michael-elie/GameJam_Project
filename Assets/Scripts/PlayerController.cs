using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float moveSpeed = 2f;
    public float playerDamage; 
    
    
    
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform BulletSpawnPosition;
    
   
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        
        
    }

    
    void Update()
    {
      moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
      moveVelocity = moveInput * moveSpeed;

      Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
      Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
      float RayLength;

      if (groundPlane.Raycast(cameraRay, out RayLength))
      {
          Vector3 pointToLook = cameraRay.GetPoint(RayLength);
          Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);
          
          
          transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
      }
      
      if (Input.GetMouseButton(0))
      {
          Fire();

      }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = moveVelocity;
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
       
       
        

    }
}
