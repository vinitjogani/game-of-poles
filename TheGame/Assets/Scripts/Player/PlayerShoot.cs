using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject ballPrefab;
    public float reloadTime = 0.5f;

    private float reload = 0;
    private GameObject oldObj;
    private Color oldColor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (reload <= 0)
        {
            if (Input.GetAxis("Fire1") > 0)
            {
                Fire(Pole.NORTH);
            }
            else if (Input.GetAxis("Fire2") > 0)
            {
                Fire(Pole.SOUTH);
            }
        }


        if (reload > 0)
        {
            reload -= Time.deltaTime;
        }
    }

    void Fire(Pole type)
    {
        var bullet = Instantiate(ballPrefab);
        bullet.transform.position = transform.position + transform.forward;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Ball>().type = type;
        reload = reloadTime;
    }
}
