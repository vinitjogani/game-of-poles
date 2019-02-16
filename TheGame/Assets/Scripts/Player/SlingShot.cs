using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;



public class SlingShot : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform offset;
    public float speed;
    public float reloadTime = 10f;

    private float reload = 0f;
    private bool shooting = false;
    private GameObject bullet;
    private Rigidbody body;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Init();
    }

    Tuple<Vector3,Vector3> Hands()
    {
        var head = InputTracking.GetLocalPosition(XRNode.Head);
        var lhand = InputTracking.GetLocalPosition(XRNode.LeftHand);
        var rhand = InputTracking.GetLocalPosition(XRNode.RightHand);

        var lPos = lhand - head;
        var rPos = rhand - head;

        return Tuple.Create(lPos, rPos);
    }

    void Init()
    {
        bullet = Instantiate(ballPrefab);
        body = bullet.GetComponent<Rigidbody>();
        body.isKinematic = true;
        bullet.transform.SetParent(transform);
        bullet.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        var down = Input.GetAxis("Axis10") != 0 || Input.GetAxis("Fire2") != 0;

        var (lPos, rPos) = Hands();

        var forward = lPos - rPos;

        if (bullet != null)
        {
            bullet.transform.localRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
        }

        if (!shooting && bullet != null)
        {
            bullet.transform.localPosition = offset.localPosition + lPos;
        }

        if (!shooting && down && forward.magnitude < 3 && reload <= 0 && bullet != null)
        {
            shooting = true;
        }

        if (shooting)
        {
            bullet.transform.localPosition = offset.localPosition + rPos;
        }

        if (shooting && !down)
        {
            bullet.transform.localPosition = offset.localPosition + rPos;

            body.isKinematic = false;
            body.useGravity = true;
            body.mass = 0.001f;
            body.velocity = bullet.transform.forward * speed;

            bullet.transform.SetParent(transform.parent);

            shooting = false;
            reload = reloadTime;
            bullet = null;
        }

        if (reload > 0)
            reload -= Time.deltaTime;
        else if (bullet == null)
            Init();


    }
}
