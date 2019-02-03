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

        //RaycastHit hit;
        //if(Physics.Raycast(transform.position, transform.forward, out hit))
        //{
        //    var obj = hit.collider.gameObject;
        //    if((oldObj == null || obj.name != oldObj.name) && !obj.CompareTag("Bullet"))
        //    {
        //        if (oldObj != null)
        //        {
        //            oldObj.GetComponent<Renderer>().material.color = oldColor;
        //        }

        //        var render = obj.GetComponent<Renderer>();
        //        oldColor = render.material.color;
        //        render.material.color = Color.red;

        //        oldObj = obj;
        //    }
        //}
        //else
        //{
        //    if (oldObj != null)
        //    {
        //        var render = oldObj.GetComponent<Renderer>();
        //        render.material.color = oldColor;

        //        oldObj = null;
        //    }
        //}
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
