using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject moveplane;
    public GameObject shootplane;
    public GameObject attractplane;
    public int objective;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Ball(Clone)" && objective == 1)
        {
            Debug.Log("Ball hit");
            Destroy(shootplane);
            Destroy(collider);
        }
        else if (collider.name == "Ball(Clone)" && objective == 0)
        {
            
            Destroy(moveplane);
            Destroy(collider);
        }
        else if (collider.name == "Player" && objective == 0) {
            Destroy(moveplane);
        }
        else if (collider.name == "AttractCube" && objective == 3)
        {
            Destroy(attractplane);
        }
    }
}
