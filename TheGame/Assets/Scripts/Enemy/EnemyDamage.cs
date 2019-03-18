using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public GameObject deathAudio;
    public float maxHealth = 100f;
    public Collider newCollider, oldCollider;

    private float health;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnDestroy()
    {
        var manager = FindObjectOfType<MagnetManager>();
        if (manager)
        {
            manager.objects.RemoveAll(x => x.obj == gameObject);
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            Instantiate(deathAudio).transform.position = transform.position;

            GetComponent<Actions>().Death();
            Destroy(transform.GetComponentInChildren<Slider>().gameObject);

            newCollider.enabled = true;
            oldCollider.enabled = false;
            enabled = false;

            var ai = GetComponent<EnemyAI>();
            if (ai) ai.enabled = false;

            var particles = (GameObject)Resources.Load("EnemyKill");
            var system = Instantiate(particles);
            system.transform.position = transform.position;
            system.transform.parent = transform;
            system.GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Floor") && !collision.gameObject.name.ToLower().Contains("floor") && collision.relativeVelocity.magnitude > 0.5f && health > 0f)
        {
            var body = collision.gameObject.GetComponent<Rigidbody>();
            float mass = body ? body.mass : 1;

            if (collision.relativeVelocity.magnitude > 0.5f)
            {
                GetComponent<Actions>().Damage();
            }

            // Kinetic energy = damage
            health -= 0.5f * mass * Mathf.Pow(collision.relativeVelocity.magnitude, 2);
            var slider = transform.GetComponentInChildren<Slider>();
            if (slider) slider.value = health / maxHealth;

            // Play damage sound
            AudioSource temp = GetComponent<AudioSource>();
            AudioSource laudio = temp ? temp : gameObject.AddComponent<AudioSource>();
            laudio.volume = (1 - 1 / collision.relativeVelocity.sqrMagnitude)/2;
            Debug.Log(laudio.volume + ", " + collision.gameObject.name + ", " + collision.relativeVelocity.magnitude);
            laudio.PlayOneShot((AudioClip)Resources.Load("Explosion"));
        }
    }


    bool InSight()
    {
        var camPos = Camera.main.transform.position;

        RaycastHit hit;
        if (Physics.Raycast(camPos, transform.position - camPos, out hit))
            return hit.collider.gameObject == gameObject;
        return false;
    }
}
