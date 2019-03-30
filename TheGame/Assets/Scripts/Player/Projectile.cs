using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private LineRenderer lr;
    //public GameObject ammo;
    public float initialVelocity;
    private float angle, newangle, radian;
    public float gravity;
    private float distance, height;
    private int flag = 0;
    private Collider collider = new Collider();

    private void Start()
    {
        lr = gameObject.AddComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {

        //newangle = Vector3.Angle(Camera.main.transform.position - new Vector3(0f, 0f, 2f), Camera.main.transform.forward);
        ////Debug.Log(newangle);
        //radian = (newangle * (22 / 7)) / 180;
        //height = (initialVelocity * initialVelocity * Mathf.Sin(radian) * Mathf.Sin(radian)) / (2 * gravity);
        //distance = (initialVelocity * initialVelocity * Mathf.Sin(2 * radian)) / gravity;
        //lr.material = new Material(Shader.Find("Sprites/Default"));

        //// Set some positions
        //Vector3[] positions = new Vector3[3];
        //lr.numCornerVertices = 4;
        //lr.startWidth = 0.1f;
        //lr.endWidth = 0.1f;
        //RaycastHit hit;
        //var rotation = ((Camera.main.transform.rotation.z) * (22 / 7)) / 180;
        //positions[0] = Camera.main.transform.position + Camera.main.transform.forward;
        //positions[1] = Camera.main.transform.position + Camera.main.transform.forward + new Vector3((0.0f * Mathf.Cos(rotation)) + ((distance / 2f) * Mathf.Sin(rotation)), height, -(0.0f * Mathf.Sin(rotation)) + ((distance / 2f) * Mathf.Cos(rotation)));
        //positions[2] = Camera.main.transform.position + Camera.main.transform.forward + new Vector3((0.0f * Mathf.Cos(rotation)) + ((distance) * Mathf.Sin(rotation)), 0f, -(0.0f * Mathf.Sin(rotation)) + ((distance) * Mathf.Cos(rotation)));
        //lr.positionCount = positions.Length;
        //lr.SetPositions(positions);
        //if (Physics.Linecast(positions[0], positions[1], out hit))
        //{
        //    flag = 1;
        //    collider = hit.collider;

        //    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
        //}
        //else
        //{
        //    if (Physics.Linecast(positions[1], positions[2], out hit))
        //    {
        //        flag = 1;
        //        collider = hit.collider;
        //        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
        //    }
        //    else
        //    {
        //        if (flag == 1)
        //        {
        //            flag = 2;
        //        }
        //    }
        //}

        //if (flag == 2)
        //{
        //    collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
        //}
    }
}
