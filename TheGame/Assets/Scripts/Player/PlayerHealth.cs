using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerState {
    public static Vector3? respwanAt = null;
    public static Quaternion? rotation = null;
    public static int level = -1;

}

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;

    private Texture2D black;
    private float health;
    private AudioSource lAudio;

    private bool restartLevel = false;

    private void Start()
    {
        if (PlayerState.respwanAt != null && SceneManager.GetActiveScene().buildIndex == PlayerState.level)
        {
            transform.position = PlayerState.respwanAt.Value;
            Camera.main.transform.rotation = PlayerState.rotation.Value;

            lAudio = gameObject.AddComponent<AudioSource>();
            lAudio.PlayOneShot((AudioClip)Resources.Load("respawn"));
        }

        PlayerState.level = SceneManager.GetActiveScene().buildIndex;
        health = maxHealth;

        black = new Texture2D(1, 1);
        black.SetPixel(0, 0, new Color(0, 0, 0, 0));
        black.Apply();
    }

    private void Update()
    {
        if (restartLevel && !lAudio.isPlaying)
        {
            SceneManager.LoadScene(PlayerState.level);
        }

        PlayerState.respwanAt = transform.position;
        PlayerState.rotation = Camera.main.transform.rotation;
    }

    public void TakeDamage(float damage)
    {
        if (health > 15 && health - damage < 15)
        {
            lAudio = gameObject.AddComponent<AudioSource>();
            lAudio.PlayOneShot((AudioClip)Resources.Load("ohDeath"));
        }

        health -= damage;
        black.SetPixel(0, 0, new Color(0, 0, 0, 1 - health / maxHealth));
        black.Apply();

        if (health <= 0) restartLevel = true;
    }


    void OnGUI()
    {         
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
    }
}
