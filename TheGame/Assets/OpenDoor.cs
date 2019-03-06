using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject plane;

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        Destroy(plane);

    }
}
