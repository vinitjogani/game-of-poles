using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public int numberOfWaves;
    public List<int> numberOfEnemiesPerWave;
    public GameObject enemy;

    public float randTimeLow = 0f;
    public float randTimeHigh = 7f;
    public float randEnemyNumLow = 3f;
    public float randEnemyNumHigh = 7f;
    public static float score = 0f;

    private GameObject[] enemySpawns;
    private int wavesSpawned = 0;
    private List<GameObject> instantiated = new List<GameObject>();
    private List<GameObject> deleted = new List<GameObject>();
    private List<float> timesToSpawn = new List<float>();
    private float totalTime = 0f;
    private int spawnIndex = 0;
    private float timeToDelete = 0f;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;

        if (instantiated.Count == 0 && wavesSpawned < numberOfWaves)
        {
            if (enemySpawns == null)
            {
                enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
            }

            for (int i=0; i < numberOfEnemiesPerWave[wavesSpawned]; i++)
            {
                GameObject enemySpawn = enemySpawns[spawnIndex % enemySpawns.Length];
                var clone = Instantiate(enemy);
                instantiated.Add(clone);
                Vector3 position = new Vector3(enemySpawn.transform.position.x, 
                    enemySpawn.transform.position.y + 2, enemySpawn.transform.position.z);
                clone.transform.position = position;

                spawnIndex++;
            }

            wavesSpawned++;
        
        } else if (instantiated.Count == 0 && wavesSpawned >= numberOfWaves)
        {
            if (enemySpawns == null)
            {
                enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
            }

            for (int i = 0; i < UnityEngine.Random.Range(randEnemyNumLow, randEnemyNumHigh); i++)
            {
                timesToSpawn.Add(UnityEngine.Random.Range(randTimeLow, randTimeHigh));
            }

            wavesSpawned++;
        }

        totalTime += Time.deltaTime;

        if (totalTime >= timeToDelete && deleted.Count > 0)
        {
            Destroy(deleted[0]);
            deleted.RemoveAt(0);
        }

        foreach (float timeToSpawn in timesToSpawn)
        {
            if (totalTime >= timeToSpawn)
            {
                GameObject enemySpawn = enemySpawns[spawnIndex % enemySpawns.Length];
                var clone = Instantiate(enemy);
                instantiated.Add(clone);
                Vector3 position = new Vector3(enemySpawn.transform.position.x,
                                enemySpawn.transform.position.y + 2, enemySpawn.transform.position.z);
                clone.transform.position = position;

                spawnIndex++;
            }

            timesToSpawn.Remove(timeToSpawn);
        }
    }

    void FixedUpdate()
    {
        foreach (var obj in instantiated)
        {
            if (!obj.GetComponent<EnemyAI>().enabled)
            {
                Debug.Log("got disabled enemy");
                score += 100f;
                deleted.Add(obj);
            }
        }

        foreach (var obj in deleted)
        {
            instantiated.Remove(obj);
            timeToDelete = totalTime + UnityEngine.Random.Range(randTimeLow, randTimeHigh);
        }
    }
}
