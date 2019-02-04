using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilizeCollider : MonoBehaviour
{
    void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
