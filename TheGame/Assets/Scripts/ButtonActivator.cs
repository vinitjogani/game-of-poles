using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivator : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        gameObject.AddComponent<Rigidbody>();
        GetComponent<AudioSource>().Play();
    }
}
