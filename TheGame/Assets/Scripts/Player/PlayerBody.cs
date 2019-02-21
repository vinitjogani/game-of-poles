using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
