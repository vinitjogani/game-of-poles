using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScores : MonoBehaviour
{

    public Text scoreText, highScoreText;

    // Start is called before the first frame update
    void Start()
    {

        string scoreString = (WaveSpawner.highScore).ToString();
        while (scoreString.Length < 4)
            scoreString = "0" + scoreString;
        highScoreText.text = "HIGHEST: " + scoreString;


        scoreString = ((int)WaveSpawner.score).ToString();
        while (scoreString.Length < 4)
            scoreString = "0" + scoreString;
        scoreText.text = "SCORE: " + scoreString;

    }

}
