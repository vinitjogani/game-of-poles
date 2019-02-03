﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage = 10f;
    public float bulletSpeed = 10f;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    private void OnDestroy()
    {
        var manager = GameObject.FindObjectOfType<MagnetManager>();
        manager.objects.RemoveAll(x => x.obj == gameObject);
    }

    // If the colliding object has health, deduct.
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
