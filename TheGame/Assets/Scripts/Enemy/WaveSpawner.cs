using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public int numberOfWaves;
    public int numberOfEnemiesPerWave;
    public GameObject enemy;

    private GameObject[] enemySpawns;
    private int wavesSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy");
        if ((enemiesAlive.Length == 0 || enemiesAlive == null) && wavesSpawned < numberOfWaves)
        {
            if (enemySpawns == null)
            {
                enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
            }

            for (int i=0; i < numberOfEnemiesPerWave; i++)
            {
                GameObject enemySpawn = enemySpawns[i % enemySpawns.Length];
                Instantiate(enemy);
                enemy.transform.position = enemySpawn.transform.position;
            }

            wavesSpawned++;
        }
    }
}
