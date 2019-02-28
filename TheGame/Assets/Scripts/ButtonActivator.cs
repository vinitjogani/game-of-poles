using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivator : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        var body = gameObject.AddComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        body.velocity = Vector3.down * 5f;
        GetComponent<AudioSource>().Play();
    }
}
