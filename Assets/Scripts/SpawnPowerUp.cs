using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    public GameObject Target;
    public Vector3 TargetRotation;
    private void Start()
    {
        
    }

    
    IEnumerator DelayBeforeSpawn()
    {

        yield return new WaitForSeconds(1f);
        Vector3 RadomSpawnPosition = new Vector3(Random.Range(-355f,355f), 0.3f, Random.Range(-355f, 355f));
        GameObject temp = Instantiate(Target, RadomSpawnPosition, Quaternion.Euler(TargetRotation));
        temp.GetComponent<PowerUp>().SpawnerScript = this;

    }
    
    public void respawn (string name)

    {
        if (name == gameObject.name)
        {
            StartCoroutine(DelayBeforeSpawn());
        }
           
    }

    private void OnEnable()
    { 
        PowerUp.OnTargetTouched += respawn;
    }
   
    private void OnDisable()
    {
        PowerUp.OnTargetTouched -= respawn;
    }
    
   
   
}

