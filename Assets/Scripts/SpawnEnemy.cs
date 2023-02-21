using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour

{
    public WaveEnemy[] waves;
 
    private WaveEnemy currentWave;
 
    [SerializeField]
    private Transform[] spawnpoints;
 
    private float timeBtwnSpawns;
    private int i = 0;
 
    private bool stopSpawning = false;
 
    private void Awake()
    {
 
        currentWave = waves[i];
        timeBtwnSpawns = currentWave.TimeBeforeThisWave;
    }
 
    private void Update()
    {
        if(stopSpawning)
        {
            return;
        }
 
        if (Time.time >= timeBtwnSpawns)
        {
            SpawnWave();
            IncWave();
 
            timeBtwnSpawns = Time.time + currentWave.TimeBeforeThisWave;
        }
    }
 
    private void SpawnWave()
    {
        for (int i = 0; i < currentWave.NumberToSpawn; i++)
        {
            int num = Random.Range(0, currentWave.EnemiesInWave.Length);
            int num2 = Random.Range(0, spawnpoints.Length);
 
        /* GameObject temp =  */ Instantiate(currentWave.EnemiesInWave[num], spawnpoints[num2].position, spawnpoints[num2].rotation);
         //temp.GetComponent<EnemyController>().Playertarget = this.gameObject;

        }
    }
 
    private void IncWave()
    {
        if (i + 1 < waves.Length)
        {
            i++;
            currentWave = waves[i];
        }
        else
        {
            stopSpawning = true;
        }
    }
}