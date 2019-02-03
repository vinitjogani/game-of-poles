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
    public float shotInterval = 0.5f;
    public float health = 100f; // Current health
    public float maxHealth = 100f; // Used for health bar

    public Texture healthbarBackground;
    public Texture healthbarForeground;

    private float shotTime = 0f;

    private void Start()
    {
        if (System.Math.Abs(health - maxHealth) > 0)
        {
            Debug.Log("Warning EnemyAI has maxHealth != health (curr) at start.");
        }
    }

    // Update is called once per frame
    void Update()
    {   
        // Print a warning that the enemy will never shoot if we don't look first
        if (maximumLookDistance < maximumAttackDistance)
        {
            Debug.Log("Warning EnemyAI has maximumLookDistance < maximumAttackDistance will not shoot until player is within maximumLookDistance.");
        }

        // Die if health is 0
        if (health <= 0)
        {
            Destroy(this);
        }

        // Check if we should look at the player, if so check if we should shoot.
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= maximumLookDistance)
        {
            LookAtTarget();

            //Check distance and time
            if (distance <= maximumAttackDistance && (Time.time - shotTime) > shotInterval)
            {
                Shoot();
            }
        }
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
        shotTime = Time.time;

        // Instantiate a bullet
        Instantiate(projectile,
            transform.position + (target.transform.position - transform.position).normalized,
            Quaternion.LookRotation(target.transform.position - transform.position));
    }

    // The bullet will call this.
    void TakeDamage(float damage)
    {
        this.health -= damage;
    }

    // Display a health bar
    void OnGUI()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        // Draw Background
        GUI.DrawTexture(new Rect(screenPoint.x - 100, screenPoint.y - 20, 200, 40),
                        healthbarBackground, ScaleMode.StretchToFill, true);
        // Draw Foreground
        GUI.DrawTexture(new Rect(screenPoint.x - 100, screenPoint.y - 20, 200F * health/maxHealth, 40),
                        healthbarForeground, ScaleMode.StretchToFill, true,
                        ((healthbarForeground.width * health) / healthbarForeground.height));
    }
}
