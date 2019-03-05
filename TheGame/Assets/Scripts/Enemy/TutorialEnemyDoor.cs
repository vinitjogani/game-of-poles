using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyDoor : MonoBehaviour
{
    private bool over = false;

    public void OnDeath()
    {
        if (!over)
        {
            var body = gameObject.AddComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            GetComponent<AudioSource>().Play();
            over = true;
        }
    }
}
