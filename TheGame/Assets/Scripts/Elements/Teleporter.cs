using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool isEnabled = false;
    public float range = 50f;

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

    private void Update()
    {
        var distance = (Camera.main.transform.position - transform.position).magnitude;

        if (distance < range)
        {
            isEnabled = true;
            renderer.material.color = originalColor;
        }
        else
        {
            isEnabled = false;
            renderer.material.color = Color.red;
        }
    }
}
