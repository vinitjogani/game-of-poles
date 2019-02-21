using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        body.AddForce(transform.forward * speed);
    }

}
