using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyUp(KeyCode.RightShift)) 
        {
            GameObject newArrow = Instantiate(arrowPrefab) as GameObject;
            newArrow.transform.position = transform.position;
            Rigidbody rb = newArrow.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward*40;
        }
    }
}
