using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem explosionfx;
    public AudioSource Explosionsound;
    [SerializeField] private EnemyData _enemyData;
    public float Pv = 100f;

    void Start()
    {
        _enemyData.enemyPv = Pv;
    }


    void Update()
    {

    }

    public void Damage(float damage)
    {
        Pv = Pv - damage;
        if (Pv <= 0)
        {
            Destruction();

        }

    }

    protected virtual void Destruction()
    {

       // explosionfx.transform.SetParent(null);
      //  Explosionsound.Play();
      //  explosionfx.Play();
        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.GetComponentInParent<PlayerController>() != null)
        {

            collision.gameObject.GetComponentInParent<PlayerController>().ApplyDamage(_enemyData.enemyDamage);

        }

    }
}