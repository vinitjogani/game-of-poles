using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        var rot = Camera.main.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, rot.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            var body = collision.gameObject.GetComponent<Rigidbody>();
            if (body)
            {
                GetComponentInParent<PlayerHealth>().TakeDamage(0.5f * body.mass * collision.relativeVelocity.sqrMagnitude);
            }
        }
    }
}
