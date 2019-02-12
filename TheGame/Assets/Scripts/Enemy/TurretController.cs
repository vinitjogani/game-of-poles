﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float rotationDegreesPerSecond = 3f;

    public GameObject projectile;
    public float maximumAttackDistance = 10f;
    public float rotationDamping = 2f;
    public float shotInterval = 1f;

    private float shotTime = 0f;
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
    }

    // Instantiate a bullet towards the enemy.
    void Shoot()
    {
        //Reset the time when we shoot
        shotTime = shotInterval;

        // Instantiate a bullet forward
        var f_bullet = Instantiate(projectile);
        f_bullet.transform.position = transform.position + transform.forward * 2;

        // Instantiate a bullet backward
        var b_bullet = Instantiate(projectile);
        b_bullet.transform.position = transform.position - transform.forward * 2;

        // Instantiate a bullet left
        var l_bullet = Instantiate(projectile);
        l_bullet.transform.position = transform.position - transform.right * 2;

        // Instantiate a bullet right
        var r_bullet = Instantiate(projectile);
        r_bullet.transform.position = transform.position + transform.right * 2;
    }

    void rotate()
    {
        float currentAngle = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.AngleAxis(currentAngle + (Time.deltaTime * rotationDegreesPerSecond), Vector3.up);
    }
}