using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject projectile;

    public float maximumLookDistance = 30f;
    public float maximumAttackDistance = 10f;
    public float rotationDamping = 2f;
    public float shotInterval = 1f;

    private float shotTime = 0f;
    private Transform target;

    private void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {  
        // Check if we should look at the player, if so check if we should shoot.
        float distance = Vector3.Distance(target.position, transform.position);

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
        Vector3 dir = target.position - transform.position;
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
        bullet.transform.position = transform.position + transform.forward * 2 + transform.up * 3;
        bullet.transform.rotation = Quaternion.LookRotation(target.position - bullet.transform.position);

        GetComponent<Actions>().Attack();
    }

}
