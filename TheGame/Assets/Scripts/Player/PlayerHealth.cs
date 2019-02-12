using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerState {
    public static Vector3? respwanAt = null;
    public static Quaternion? rotation = null;
}

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;

    private Texture2D black;
    private float health;

    private void Start()
    {
        if (PlayerState.respwanAt != null)
        {
            transform.position = PlayerState.respwanAt.Value;
            Camera.main.transform.rotation = PlayerState.rotation.Value;
        }

        health = maxHealth;

        black = new Texture2D(1, 1);
        black.SetPixel(0, 0, new Color(0, 0, 0, 0));
        black.Apply();
    }

    private void Update()
    {
        PlayerState.respwanAt = transform.position;
        PlayerState.rotation = Camera.main.transform.rotation;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        black.SetPixel(0, 0, new Color(0, 0, 0, 1 - health / maxHealth));
        black.Apply();

        if (health <= 0) RestartLevel();
    }


    void OnGUI()
    {         
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("FinalTutorial");
    }
}
