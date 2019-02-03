using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pole
{
    NORTH,
    SOUTH
}

public class Ball : MonoBehaviour
{
    public float speed = 10f;
    public Pole type = Pole.NORTH;

    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        body.AddForce(transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !other.CompareTag("Player"))
        {
            MagnetObject magnetObject = new MagnetObject(other.gameObject, type, MagnetManager.magnetizeTime);
            var manager = FindObjectOfType<MagnetManager>();

            MagnetObject search = manager.objects.Find(x => x.obj.name == magnetObject.obj.name);
            if (search != null)
            {
                search.time = MagnetManager.magnetizeTime;
                search.strength *= 1.1f;
                search.pole = type;
            }
            else
            {
                manager.objects.Add(magnetObject);
            }
            Destroy(gameObject);
        }
    }
}
