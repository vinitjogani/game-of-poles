using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    private float lifeTime = 2f;
    private float timer;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>= lifeTime)
        {
            Destroy(gameObject);
        }

        if (!hit)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Arrow" && collision.collider.tag != "Player")
        {
            hit = true;
            Stick();
        }
    }

    private void Stick()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
