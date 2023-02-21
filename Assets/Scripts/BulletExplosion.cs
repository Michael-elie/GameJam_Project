using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    [SerializeField] private float BulletSpeed = 10f;
    public PlayerController PlayerController;
    public GameObject explosionfx;
    public float explosionForce, radius;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * BulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyController>() != null)
        {
           KnockBack();
           collision.gameObject.GetComponentInParent<EnemyController>().Damage(PlayerController.playerDamage + 75f);
           
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
                rigidbody.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }
    }

   

    
}