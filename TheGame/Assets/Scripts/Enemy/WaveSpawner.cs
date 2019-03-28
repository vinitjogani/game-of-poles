using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public int numberOfWaves;
    public List<int> numberOfEnemiesPerWave;
    public GameObject enemy;
    static int enemyCounter = 0;

    public float randTimeLow = 0f;
    public float randTimeHigh = 7f;
    public int randEnemyNumLow = 3;
    public int randEnemyNumHigh = 7;
    public static float score = 0f;

    private GameObject[] enemySpawns;
    private int wavesSpawned = 0;
    private List<GameObject> instantiated = new List<GameObject>();
    private List<GameObject> deleted = new List<GameObject>();
    private List<float> timesToSpawn = new List<float>();
    private float totalTime = 0f;
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
        totalTime += Time.deltaTime;

        if (instantiated.Count == 0 && timesToSpawn.Count == 0 && wavesSpawned < numberOfWaves)
        {
            Debug.Log("Spawning wave " + wavesSpawned);
            SpawnAll(numberOfEnemiesPerWave[wavesSpawned]);
        }
        else if (instantiated.Count == 0 && timesToSpawn.Count == 0 && wavesSpawned >= numberOfWaves)
        {
            Debug.Log("Spawning wave " + wavesSpawned);
            SpawnAll(Random.Range(randEnemyNumLow, randEnemyNumHigh));
        }

        if (totalTime >= timeToDelete && deleted.Count > 0)
        {
            Destroy(deleted[0]);
            deleted.RemoveAt(0);
        }

        foreach (float timeToSpawn in timesToSpawn)
        {
            if (totalTime >= timeToSpawn)
            {
                GameObject enemySpawn = enemySpawns[Random.Range(0, enemySpawns.Length)];
                var clone = Instantiate(enemy);
                clone.name = "Enemy" + enemyCounter;
                enemyCounter++;
                instantiated.Add(clone);
                clone.transform.position = enemySpawn.transform.position;
            }
        }
        timesToSpawn.RemoveAll(x => x <= totalTime);
    }

    void SpawnAll(int count)
    {
        timesToSpawn.Add(totalTime);
        for (int i = 0; i < count-1; i++)
        {
            timesToSpawn.Add(totalTime + Random.Range(0, 3));
        }

        wavesSpawned++;
    }

    void FixedUpdate()
    {
        foreach (var obj in instantiated)
        {
            if (!obj.GetComponent<EnemyAI>().enabled)
            {
                score += 100f;
                deleted.Add(obj);
            }
        }

        foreach (var obj in deleted)
        {
            instantiated.Remove(obj);
            timeToDelete = totalTime + Random.Range(randTimeLow, randTimeHigh);
        }
    }
}
