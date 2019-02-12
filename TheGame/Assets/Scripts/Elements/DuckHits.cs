using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DuckHits : MonoBehaviour
{
    private float collisions = 0;

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        if (collisions > 6)
        {
            SceneManager.LoadScene("Over");
        }
    }
}
