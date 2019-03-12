using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject moveplane;
    public GameObject shootplane;
    public GameObject attractplane;
    public int objective;

	private void Update()
	{
        var distance = (Camera.main.transform.position - transform.position).magnitude;
        if (distance < 10 && objective == 0)
        {
            Destroy(moveplane);
            Destroy(GetComponent<Collider>());
        }
            
	}

	private void OnCollisionEnter(Collision collision)
	{
        
        if (collision.gameObject.name == "ObjectiveCube" && objective == 2)
        {
            Destroy(attractplane);
        }
	}

	void OnTriggerEnter(Collider collider)
    {
        
        if (collider.CompareTag("Bullet") && objective == 1)
        {
            Destroy(shootplane);
            Destroy(collider);
        }
    }
}
