using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyDoor : MonoBehaviour
{
    public void OnDeath()
    {
        gameObject.AddComponent<Rigidbody>();
    }
}
