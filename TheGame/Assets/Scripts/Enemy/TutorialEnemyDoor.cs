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
            gameObject.AddComponent<Rigidbody>();
            GetComponent<AudioSource>().Play();
            over = true;
        }
    }
}
