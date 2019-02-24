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
    public float randomRange = 0f;
    private float lookAwayTime = 5f;

    private float shotTime = 0f;
    private Transform target;
    private float raycastTime = 0f;

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

        // Raycast to player, and if fails add time to raycastTime, if raycastTime >= lookAwayTime then look away.
        if (Physics.Raycast(transform.position, target.position - transform.position, maximumLookDistance))
        {
            raycastTime = 0f;
        }
        else
        {
            raycastTime += Time.deltaTime;
            if (raycastTime >= lookAwayTime)
            {
                Vector3 dir = target.position - transform.position - new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));
                Quaternion rotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            }
        }
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
           
        // Randomized target
        Vector3 randomVector = new Vector3(Random.Range(0, randomRange), Random.Range(0, randomRange), Random.Range(0, randomRange));
        Vector3 randomizedTarget = target.position - randomVector - bullet.transform.position;
        bullet.transform.rotation = Quaternion.LookRotation(randomizedTarget);

        GetComponent<Actions>().Attack();
    }

}
