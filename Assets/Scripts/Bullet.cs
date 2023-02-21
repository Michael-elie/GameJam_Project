using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float BulletSpeed = 10f;
    public PlayerController PlayerController;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * BulletSpeed;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyController>() != null )
        {

            collision.gameObject.GetComponentInParent<EnemyController>().Damage( PlayerController.playerDamage);
        }
        Destroy(gameObject,0.05f);
    }
}
