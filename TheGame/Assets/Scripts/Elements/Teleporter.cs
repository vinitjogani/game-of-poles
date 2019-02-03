using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool isEnabled = false;

    private Renderer renderer;
    private ParticleSystem particleSystem;
   
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        particleSystem = GetComponent<ParticleSystem>();

        renderer.enabled = false;
        particleSystem.Stop();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeleporterRange"))
        {
            isEnabled = true;
            particleSystem.Play();
            renderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TeleporterRange"))
        {
            isEnabled = false;
            particleSystem.Stop();
            renderer.enabled = false;
        }
    }
}
