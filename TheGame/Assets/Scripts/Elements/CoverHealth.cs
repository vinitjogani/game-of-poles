using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverHealth : MonoBehaviour
{
    public float health = 100f;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this);
        }
    }

    // The bullet will call this.
    void TakeDamage(float damage)
    {
        this.health -= damage;
    }
}
