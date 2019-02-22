using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuck : MonoBehaviour
{
    public float duckDistance = 1.5f;

    private float initialY;
    private bool duck = false;

    private void Update()
    {
        Duck();
    }

    void Duck()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !duck)
        {
            initialY = transform.position.y;
            duck = true;
            transform.position = new Vector3(transform.position.x, transform.position.y - duckDistance, transform.position.z);
        }
        else if (duck && !Input.GetKey(KeyCode.LeftShift))
        {
            duck = false;
            transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
        }
    }
}
