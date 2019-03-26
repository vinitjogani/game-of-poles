using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public static class PlayerState {
    public static Vector3? respwanAt = null;
    public static Quaternion? rotation = null;
    public static int level = -1;
}

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;

    private float health;
    private AudioSource lAudio;
    private PostProcessingProfile grayscaleBehaviour;
    private bool restartLevel = false;

    private void Start()
    {
        if (PlayerState.respwanAt != null && SceneManager.GetActiveScene().buildIndex == PlayerState.level)
        {
            transform.position = PlayerState.respwanAt.Value;
            Camera.main.transform.rotation = PlayerState.rotation.Value;

            AudioSource temp = GetComponent<AudioSource>();
            this.lAudio = temp ? temp : gameObject.AddComponent<AudioSource>();
            lAudio.PlayOneShot((AudioClip)Resources.Load("respawn"));
        }

        PlayerState.level = SceneManager.GetActiveScene().buildIndex;
        health = maxHealth;

        // Init grayscale
        grayscaleBehaviour = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
        ColorGradingModel.Settings cgm = grayscaleBehaviour.colorGrading.settings;
        cgm.basic.saturation = 1.5f;
        cgm.tonemapping.neutralBlackIn = 0;
        grayscaleBehaviour.colorGrading.settings = cgm;
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
        if (health > 5 && health - damage < 5)
        {
            AudioSource temp = GetComponent<AudioSource>();
            this.lAudio = temp ? temp : gameObject.AddComponent<AudioSource>();
            lAudio.PlayOneShot((AudioClip)Resources.Load("ohDeath"));
        }

        health -= damage;

        // Grayscale with health
        ColorGradingModel.Settings cgm = grayscaleBehaviour.colorGrading.settings;
        cgm.basic.saturation = Mathf.Max(2 * health / maxHealth, 0);
        cgm.tonemapping.neutralBlackIn = 0.1f * (maxHealth - health) / maxHealth;
        grayscaleBehaviour.colorGrading.settings = cgm;

        if (health <= 0)
        {
            restartLevel = true;
            Debug.Log("I died" + ", " + lAudio.isPlaying);
        }
    }
}
