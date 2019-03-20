using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    private float gameOver = 2f;
    void Update()
    {
        if (gameOver < 2f) gameOver -= Time.deltaTime;
        if (gameOver < 0f) SceneManager.LoadScene("Over");

        if (GetComponent<EnemyAI>().enabled == false) gameOver -= Time.deltaTime;
    }
}
