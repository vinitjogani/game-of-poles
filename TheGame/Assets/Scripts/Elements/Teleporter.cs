using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool isEnabled = false;

    private Renderer renderer;
    public ParticleSystem particles;

    private Color originalColor;
   
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        particles = GetComponent<ParticleSystem>();

        originalColor = renderer.material.color;
        renderer.material.color = Color.red;
        particles.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleporterRange") && !isEnabled)
        {
            isEnabled = true;
            renderer.material.color = originalColor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TeleporterRange"))
        {
            isEnabled = false;
            renderer.material.color = Color.red;
        }
    }
}
