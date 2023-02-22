using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem deathfx;
    public AudioSource deathsound;
    [SerializeField] private EnemyData _enemyData;
    private float Pv ;
    private NavMeshAgent enemy;
    private GameObject Playertarget;
    public float knockBackvalue;
    private Vector3 knockBackdistance;
   // public SpawnEnemy SpawnEnemy;
    public Animator _animator;
    

    void Start()
    {
         Pv =  _enemyData.enemyPv;
        enemy = GetComponent<NavMeshAgent>();
        enemy.speed = _enemyData.enemySpeed;
     Playertarget = GameObject.Find("PlayerAstro");
    }


    void Update()
    {
        enemy.SetDestination(Playertarget.transform.position);
       
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

        deathfx.transform.SetParent(null);
       // deathsound.Play();
       
      enemy.speed = 0f;
      _animator.SetInteger("enemyAnimation",2);
      deathfx.Play();
      Destroy(gameObject, 2f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.GetComponentInParent<PlayerController>() != null)
        {
            _animator.SetInteger("enemyAnimation",1 );
            collision.gameObject.GetComponentInParent<PlayerController>().ApplyDamage(_enemyData.enemyDamage);
            
            Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
               knockBackdistance = ((Playertarget.transform.position - transform.position).normalized)*knockBackvalue;
                rigidbody.AddForce(knockBackdistance);
               
            }
           
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        _animator.SetInteger("enemyAnimation",0 );
    }

   
}