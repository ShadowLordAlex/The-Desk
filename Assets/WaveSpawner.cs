using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    /// <summary>
    /// General Notes for this code
    /// 
    /// All time is noted in seconds!
    /// Wave is an array that stores stuff
    /// 
    /// </summary>

    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };
    
    [System.Serializable]
    public class Wave
    {   
        //name for the wave
        public string name;
        
        public Transform Enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;
    
    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    private SpawnState state = SpawnState.COUNTING;
    private float searchCountDown = 1f;

    private void Start()
    {
        
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("You forgot to add the spawnpoints you fool");
        }
        
        waveCountDown = timeBetweenWaves;
        
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            //Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                
            }
            else
            {
                return;
            }
        }
        
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));

            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All Waves done LOOPING");
        }
        else
        {
            nextWave++;
        }
    }
    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("SpawnWave");
        state = SpawnState.SPAWNING;
        
        //Spawn the boys
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.Enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;
        
        yield break;
    }

    public void SpawnEnemy(Transform _enemy)
    {
        //Spawn Enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}

