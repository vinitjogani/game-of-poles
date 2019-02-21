using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;



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
    private Pole pole;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        bullet = Instantiate(ballPrefab);
        body = bullet.GetComponent<Rigidbody>();
        body.isKinematic = true;
        bullet.transform.SetParent(transform);
        bullet.transform.localPosition = new Vector3(0, 0, 0);
        bullet.transform.localRotation = Quaternion.Euler(0, 0, 0);
        pole = Pole.NORTH;
    }

    // Update is called once per frame
    void Update()
    {
        if (bullet != null)
        {
            float selector = Input.GetAxis("Axis4");
            if (selector > 0) pole = Pole.NORTH;
            else if (selector < 0) pole = Pole.SOUTH;

            if (pole == Pole.NORTH)
            {
                bullet.GetComponentInChildren<Renderer>().material.color = Color.blue;
            }
            else if (pole == Pole.SOUTH)
            {
                bullet.GetComponentInChildren<Renderer>().material.color = Color.red;
            }
        }

        var down = Input.GetAxis("Axis10") != 0 || Input.GetAxis("Fire2") != 0;

        var forward = InputTracking.GetLocalPosition(XRNode.RightHand) - InputTracking.GetLocalPosition(XRNode.LeftHand);
        
        if (!shooting && down && forward.magnitude < 0.5 && reload <= 0 && bullet != null)
        {
            shooting = true;
        }

        if (shooting && bullet)
        {
            bullet.transform.localPosition = new Vector3(0, 0, -forward.magnitude);
        }

        if (shooting && !down && bullet)
        {
            body.isKinematic = false;
            body.useGravity = true;
            body.velocity = bullet.transform.forward * speed * forward.magnitude;
            body.mass = 0.01f;
            bullet.transform.SetParent(null);
            bullet.AddComponent<BulletCollide>();
            bullet.GetComponent<BulletCollide>().type = pole;

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
