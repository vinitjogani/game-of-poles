using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<EnemyAI>().enabled == false)
            SceneManager.LoadScene("Over");
    }
}
