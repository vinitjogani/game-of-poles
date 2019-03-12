using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject player;
    private bool paused = false;
    void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        //Debug.Log();
        if ((Input.GetKeyDown(KeyCode.P)) || (Input.GetKeyDown(KeyCode.JoystickButton2)))
        {
            if (paused == false)
            {
                paused = true;
                Pause();
            }
            else
            {
                paused = false;
                Continue();
            }
        }
        if ((Input.GetKeyDown(KeyCode.M)) || (Input.GetKeyDown(KeyCode.JoystickButton0)))
            Application.LoadLevel("MainMenu");
    }
    private void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0 and switch off music
    }
    private void Continue()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts and music again
    }
}
