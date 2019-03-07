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
    public float lookAwayTime = 5f;
    public float lookAwayRange = 20f;
    public float shotTime = 1f; // shotInterval
    public bool bypassRaycast;

    private bool didLookAway = false;
    private Transform target;
    private float raycastTime = 0f;

    private void Start()
    {
        target = Camera.main.transform;
    }

    float L()
    {
        return Random.Range(-lookAwayRange, lookAwayRange);
    }


    float R()
    {
        return Random.Range(-randomRange, randomRange);
    }
    
    // Update is called once per frame
    void Update()
    {

        // Raycast to player, and if fails add time to raycastTime, if raycastTime >= lookAwayTime then look away.
        // If it doesn't fail look at player and shoot if necessary.
        RaycastHit hit;

        var head = transform.position + Vector3.up * 3;
        Ray ray = new Ray(head, target.position - head + Vector3.down * 0.2f);

        Debug.DrawRay(head, target.position - head + Vector3.down * 0.2f);
        if ((Physics.Raycast(ray, out hit, float.MaxValue, ~0, QueryTriggerInteraction.Ignore) && hit.collider.CompareTag("Player")) || bypassRaycast)
        {
            raycastTime = 0f; // Reset raycastTime

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

                if (shotTime > 1f && shotTime - Time.deltaTime < 1f && !GetComponent<Animator>().GetBool("Aiming"))
                {
                    GetComponent<Actions>().Attack();
                }
            }

            shotTime -= Time.deltaTime;
        }
        else
        {
            raycastTime += Time.deltaTime;
            if (raycastTime >= lookAwayTime && !didLookAway)
            {
                didLookAway = true;
                Vector3 dir = target.position - transform.position - new Vector3(L(), 0, L());
                Quaternion rotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            }
        }
    }

    // Look at the target
    void LookAtTarget()
    {
        didLookAway = false;

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

        var yOffset = (bullet.transform.position - Camera.main.transform.position).y;

        // Randomized target
        var distance = (Camera.main.transform.position - bullet.transform.position).magnitude;
        Vector3 randomizedTarget = transform.forward * distance - new Vector3(R(), yOffset, R()) + Vector3.down * .5f;
        bullet.transform.rotation = Quaternion.LookRotation(randomizedTarget);

        GetComponent<Actions>().Attack();

        // Play shooting sound
        AudioSource temp = GetComponent<AudioSource>();
        AudioSource laudio = temp ? temp : gameObject.AddComponent<AudioSource>();
        laudio.volume = 0.1f;
        laudio.PlayOneShot((AudioClip)Resources.Load("EnemyBulletShoot"));
    }

}
