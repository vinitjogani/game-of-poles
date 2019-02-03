using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Transform t = Camera.main.transform;
        transform.position = t.forward * 2 + t.position;
        transform.localRotation = t.localRotation;
    }
}
