using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float rotationDegreesPerSecond = 3f;

    public GameObject projectile;
    public float maximumAttackDistance = 10f;
    public float rotationDamping = 2f;
    public float shotInterval = 1f;
    public float randomRange = 0f;

    private float shotTime = 2f;
    private Transform target;

    // Update is called once per frame
    void Update()
    {
        target = Camera.main.transform;

        //Check distance and time
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= maximumAttackDistance && shotTime <= 0)
        {
            Shoot();
        }

        shotTime -= Time.deltaTime;

        rotate();

        Shoot();

    }

    // Instantiate a bullet towards the enemy.
    void Shoot()
    {
        //Reset the time when we shoot
        shotTime = shotInterval;

        // Randomized target
        Vector3 randomVector = new Vector3(Random.Range(0, randomRange), Random.Range(0, randomRange), Random.Range(0, randomRange));

        // Instantiate a bullet forward
        var f_bullet = Instantiate(projectile);
        f_bullet.transform.position = transform.position + transform.forward * 2 + randomVector;

        // Instantiate a bullet backward
        var b_bullet = Instantiate(projectile);
        b_bullet.transform.position = transform.position - transform.forward * 2 + randomVector;

        // Instantiate a bullet left
        var l_bullet = Instantiate(projectile);
        l_bullet.transform.position = transform.position - transform.right * 2 + randomVector;

        // Instantiate a bullet right
        var r_bullet = Instantiate(projectile);
        r_bullet.transform.position = transform.position + transform.right * 2 + randomVector;

        f_bullet.GetComponent<Rigidbody>().AddForce(f_bullet.transform.forward * 20);
        b_bullet.GetComponent<Rigidbody>().AddForce(b_bullet.transform.forward * 20);
        l_bullet.GetComponent<Rigidbody>().AddForce(l_bullet.transform.forward * 20);
        r_bullet.GetComponent<Rigidbody>().AddForce(r_bullet.transform.forward * 20);

        // Play shooting sound
        AudioSource temp = GetComponent<AudioSource>();
        AudioSource laudio = temp ? temp : gameObject.AddComponent<AudioSource>();
        laudio.PlayOneShot((AudioClip)Resources.Load("EnemyBulletShoot"));
    }

    void rotate()
    {
        float currentAngle = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.AngleAxis(currentAngle + (Time.deltaTime * rotationDegreesPerSecond), Vector3.up);
    }
}
