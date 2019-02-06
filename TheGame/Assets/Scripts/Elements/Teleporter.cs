using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool isEnabled = false;

    private Renderer renderer;
    private ParticleSystem particleSystem;

    private Color originalColor;
   
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        particleSystem = GetComponent<ParticleSystem>();

        originalColor = renderer.material.color;
        renderer.material.color = Color.red;
        particleSystem.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleporterRange") && !isEnabled)
        {
            isEnabled = true;
            particleSystem.Play();
            renderer.material.color = originalColor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TeleporterRange"))
        {
            isEnabled = false;
            particleSystem.Stop();
            renderer.material.color = Color.red;
        }
    }
}
