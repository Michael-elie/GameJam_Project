using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletFreeze : MonoBehaviour
{
    [SerializeField] private float BulletSpeed = 10f;
    public GameObject Freeezefx;
    public float FreezeForce, radius;
    private Vector3 fxposition;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * BulletSpeed;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyController>() != null )
        {
            KnockBack();
            collision.gameObject.GetComponent<NavMeshAgent>().speed = 0.1f;
            fxposition = collision.gameObject.transform.position;
            Instantiate(Freeezefx, fxposition, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void KnockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius: 10f);

        foreach (Collider nearby in colliders)
        {
          
            Rigidbody rigidbody = nearby.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
               rigidbody.AddExplosionForce(FreezeForce, transform.position,radius);
               
            }
        }
    }
}
