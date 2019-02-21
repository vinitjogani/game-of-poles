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
    }

    // Update is called once per frame
    void Update()
    {
        var down = Input.GetAxis("Axis10") != 0 || Input.GetAxis("Fire2") != 0;

        var forward = InputTracking.GetLocalPosition(XRNode.RightHand) - InputTracking.GetLocalPosition(XRNode.LeftHand);
        
        if (!shooting && down && forward.magnitude < 1 && reload <= 0 && bullet != null)
        {
            shooting = true;
        }

        if (shooting)
        {
            bullet.transform.localPosition = new Vector3(0, 0, -forward.magnitude);
        }

        if (shooting && !down)
        {
            body.isKinematic = false;
            body.useGravity = true;
            body.velocity = bullet.transform.forward * speed;
            body.mass = 0.01f;
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
