using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage = 10f;

    // If the colliding object has health, deduct.
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("TakeDamage", damage);
    }
}
