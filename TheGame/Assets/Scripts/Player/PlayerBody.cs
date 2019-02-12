using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    void Update()
    {
        transform.position = Camera.main.transform.position;
    }


    private void OnCollisionEnter(Collision collision)
    {
        var body = collision.gameObject.GetComponent<Rigidbody>();
        if (body)
        {
            GetComponentInParent<PlayerHealth>().TakeDamage(0.5f * body.mass * collision.relativeVelocity.sqrMagnitude);
        }
    }
}
