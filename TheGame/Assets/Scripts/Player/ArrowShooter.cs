using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public float maxMass = 1f;
    public float minMass = 0.05f;
    public float stepMass = 0.25f;
    public float thresholdMass = 0.9f;

    public GameObject arrowPrefab;
    private float shootMass = 2f;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightShift))
        {
            if (shootMass > maxMass) shootMass = maxMass;
            shootMass = Mathf.Max(minMass, shootMass - Time.deltaTime * stepMass);
        }
        else if (shootMass < thresholdMass)
        {
            var bullet = Instantiate(arrowPrefab);
            bullet.transform.position = transform.position + transform.forward;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<Rigidbody>().mass = shootMass;

            shootMass = 2 * maxMass;
        }
    }
}
