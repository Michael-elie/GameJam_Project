using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    
    public SpawnPowerUp SpawnerScript;
     
    

  

    public delegate void TargetEvents(string name);

    public static event TargetEvents OnTargetTouched;

    private void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // SpawnerScript.respawn();
            
            
            OnTargetTouched?.Invoke(SpawnerScript.gameObject.name);
            Destroy(gameObject);
            
        }  
    }
}
