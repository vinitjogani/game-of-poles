using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractCube : MonoBehaviour
{
    public GameObject plane;

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "ObjectiveCube")
            Destroy(plane);
    }
}
