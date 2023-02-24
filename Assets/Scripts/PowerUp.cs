using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    
    public SpawnPowerUp SpawnerScript;
    private AudioSource powerupsound;
    public PlayerController playerController;

  

    public delegate void TargetEvents(string name);

    public static event TargetEvents OnTargetTouched;

    private void Start()
    {
       powerupsound = GameObject.Find("PowerUpsound").GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            powerupsound.Play();
            OnTargetTouched?.Invoke(SpawnerScript.gameObject.name);
            Destroy(gameObject);
        }  
    }
}
