using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SlingShot : MonoBehaviour
{
    public GameObject ballPrefab;
    public float speed;

    private bool shooting = false;
    private GameObject bullet;
    private Rigidbody body;
    
    // Start is called before the first frame update
    void Start()
    {
        bullet = Instantiate(ballPrefab);
        body = bullet.GetComponent<Rigidbody>();
        body.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lPos = InputTracking.GetLocalPosition(XRNode.LeftHand);
        Vector3 rPos = InputTracking.GetLocalPosition(XRNode.RightHand);
        var forward = lPos - rPos;

        if (!shooting)
        {
            Debug.Log("Not shooting");
            bullet.transform.position = lPos;
        }

        if (!shooting && Input.GetAxis(7) && forward.magnitude < 3)
        {
            shooting = true;
        }

        if (shooting)
        {
            bullet.transform.position = rPos;
            Debug.Log("Shooting");
        }

        if (shooting && !Input.GetMouseButtonDown(7))
        {
            Debug.Log("Shot");

            bullet.transform.rotation = Quaternion.LookRotation(forward);
            bullet.transform.position = rPos;

            body.isKinematic = false;
            body.velocity = forward * speed;

            shooting = false;
        }
        
    }
}
