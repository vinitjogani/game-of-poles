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

    bool Ignore(Collision collision)
    {
        return collision.gameObject.CompareTag("Floor") || collision.gameObject.name.ToLower().Contains("floor");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (health > 0f)
        {
            var body = collision.gameObject.GetComponent<Rigidbody>();
            float mass = body ? body.mass : 1;
            // Kinetic energy = damage
            var hdiff = collision.transform.position.y - transform.position.y;
            if ((!Ignore(collision) || hdiff > 0) && collision.relativeVelocity.magnitude > 0.5f)
            {
                health -= 0.5f * mass * Mathf.Pow(collision.relativeVelocity.magnitude, 2);

                if (collision.relativeVelocity.magnitude > 0.5f)
                {
                    GetComponent<Actions>().Damage();
                }

                var slider = transform.GetComponentInChildren<Slider>();
                if (slider) slider.value = health / maxHealth;

                // Play damage sound
                if (!gameObject.GetComponent<CollisionSound>()) gameObject.AddComponent<CollisionSound>();
            }
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
