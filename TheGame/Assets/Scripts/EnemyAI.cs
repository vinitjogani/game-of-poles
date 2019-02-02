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
    public float health = 100f;
 
    private float shotTime = 0f;

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
}
