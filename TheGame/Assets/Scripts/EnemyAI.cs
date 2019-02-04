using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public GameObject projectile;

    public float maximumLookDistance = 30f;
    public float maximumAttackDistance = 10f;
    public float rotationDamping = 2f;
    public float shotInterval = 1f;
    public float maxHealth = 100f;

    public Texture healthbarBackground;
    public Texture healthbarForeground;

    private float shotTime = 0f;
    private float health;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnDestroy()
    {
        var manager = GameObject.FindObjectOfType<MagnetManager>();
        manager.objects.RemoveAll(x => x.obj == gameObject);
    }

    // Update is called once per frame
    void Update()
    {  
        if (health <= 0) Destroy(gameObject);

        // Check if we should look at the player, if so check if we should shoot.
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= maximumLookDistance)
        {
            LookAtTarget();

            //Check distance and time
            if (distance <= maximumAttackDistance && shotTime <= 0)
            {
                Shoot();
            }
        }

        shotTime -= Time.deltaTime;
    }

    // Look at the target
    void LookAtTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    // Instantiate a bullet towards the enemy.
    void Shoot()
    {
        //Reset the time when we shoot
        shotTime = shotInterval;

        // Instantiate a bullet
        var bullet = Instantiate(projectile);
        bullet.transform.position = transform.position + transform.forward * 2 + transform.up;
        bullet.transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }

    // Display a health bar
    void OnGUI()
    {
        var p = Camera.main.WorldToScreenPoint(transform.position);
        p.y = Screen.height - p.y;

        var dist = (transform.position - Camera.main.transform.position).magnitude;
        var size = Mathf.Max(5f, 20f - dist);

        GUI.DrawTexture(new Rect(p.x - size * 10, p.y - 100, size*20, size),
                        healthbarBackground, ScaleMode.StretchToFill, true);
        // Draw Foreground
        GUI.DrawTexture(new Rect(p.x - size*10, p.y - 100, size*20 * health / maxHealth, size),
                        healthbarForeground, ScaleMode.StretchToFill, true,
                        ((healthbarForeground.width * health) / healthbarForeground.height));
    }

    private void OnCollisionEnter(Collision collision)
    {
        var body = collision.gameObject.GetComponent<Rigidbody>();
        float mass = body ? body.mass : 1;

        // Kinetic energy = damage
        this.health -= 0.5f * mass * Mathf.Pow(collision.relativeVelocity.magnitude, 2);
    }
}
