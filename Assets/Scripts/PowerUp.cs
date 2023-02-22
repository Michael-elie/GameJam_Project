using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    
    public SpawnPowerUp SpawnerScript;
  //  private AudioSource powerup; 
    

  

    public delegate void TargetEvents(string name);

    public static event TargetEvents OnTargetTouched;

    private void Start()
    {
      //  powerup = GameObject.Find("PowerUpsound").GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // SpawnerScript.respawn();
            
         //  powerup.Play();
            OnTargetTouched?.Invoke(SpawnerScript.gameObject.name);
            Destroy(gameObject);
            
        }  
    }
}
