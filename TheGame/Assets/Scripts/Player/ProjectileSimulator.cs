using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSimulator : MonoBehaviour
{
    private static GameObject old = null;
    private static List<Color> color;

    private GameObject last = null;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 150f);
    }

    private void OnDestroy()
    {
        if (last != null)
        {

            if (old != null)
            {
                old.GetComponent<Light>().enabled = false;
            }

            old = last;
            var oldLight = old.GetComponent<Light>();
            var light = oldLight ? oldLight : old.AddComponent<Light>();

            light.enabled = true;
            light.type = LightType.Point;
            light.range = 3f;
            light.intensity = 3f;
            light.color = Color.yellow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.isTrigger)
        {
            last = other.gameObject;
            Destroy(gameObject);
        }

    }
}
