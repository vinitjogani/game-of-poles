using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public int numberOfWaves;
    public List<int> numberOfEnemiesPerWave;
    public GameObject enemy;

    public float randEnemyNumLow = 3f;
    public float randEnemyNumHigh = 7f;
    public static float score = 0f;

    private GameObject[] enemySpawns;
    private int wavesSpawned = 0;
    private List<GameObject> instantiated = new List<GameObject>();
    private List<GameObject> deleted = new List<GameObject>();
    private float deltaTime = 0f;

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
                GameObject enemySpawn = enemySpawns[i % enemySpawns.Length];
                var clone = Instantiate(enemy);
                instantiated.Add(clone);
                Vector3 position = new Vector3(enemySpawn.transform.position.x, 
                    enemySpawn.transform.position.y + 2, enemySpawn.transform.position.z);
                clone.transform.position = position;
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
                GameObject enemySpawn = enemySpawns[i % enemySpawns.Length];
                var clone = Instantiate(enemy);
                instantiated.Add(clone);
                Vector3 position = new Vector3(enemySpawn.transform.position.x,
                    enemySpawn.transform.position.y + 2, enemySpawn.transform.position.z);
                clone.transform.position = position;
            }

            wavesSpawned++;
        }

        deltaTime += Time.deltaTime;

        if (deltaTime > 5f)
        {
            Destroy(deleted[0]);
            deleted.removeAt(0);
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
            Debug.Log("removed enemy");
            instantiated.Remove(obj);
        }
    }
}
