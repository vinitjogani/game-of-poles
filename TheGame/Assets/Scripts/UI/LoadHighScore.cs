using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadHighScore : MonoBehaviour
{
    public Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        string scoreString = (WaveSpawner.highScore).ToString();
        while (scoreString.Length < 4)
            scoreString = "0" + scoreString;
        highScoreText.text = "HIGH SCORE\n" + scoreString;
    }

}
