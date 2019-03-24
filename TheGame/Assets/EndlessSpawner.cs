using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour
{
    public GameObject mover;
    private List<GameObject> instantiated = new List<GameObject>();

    public float randLow = 2f;
    public float randHigh = 5f;

    private float randomTimer = 0f;
    public static float score = 0f;

    // Update is called once per frame
    void Update()
    {
        if (randomTimer <= 0f)
        {
            var clone = Instantiate(mover);
            instantiated.Add(clone);
            clone.transform.position = transform.position;
            randomTimer = UnityEngine.Random.Range(randLow, randHigh);
        }

        randomTimer -= Time.deltaTime;
        score += Time.deltaTime;
    }

    void FixedUpdate()
    {
        Debug.Log((int)score);

        List<GameObject> deleted = new List<GameObject>();

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
        }
    }
}
