using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public int numberOfWaves;
    public List<int> numberOfEnemiesPerWave;
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

            for (int i=0; i < numberOfEnemiesPerWave[wavesSpawned]; i++)
            {
                GameObject enemySpawn = enemySpawns[i % enemySpawns.Length];
                Instantiate(enemy);
                Vector3 position = new Vector3(enemySpawn.transform.position.x, 
                    enemySpawn.transform.position.y + 2, enemySpawn.transform.position.z);
                enemy.transform.position = position;
            }

            wavesSpawned++;
        }
    }
}
