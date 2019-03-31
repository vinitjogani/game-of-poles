using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour
{

    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        string scoreString = ((int)WaveSpawner.score).ToString();
        while (scoreString.Length < 4)
            scoreString = "0" + scoreString;
        scoreText.text = "CURRENT SCORE\n" + scoreString;
    }

}
