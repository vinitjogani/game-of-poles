using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public float teleportSpeed = 2f;

    private float alpha = 0f;
    private float direction = 0f;
    private Texture2D black;

    private Transform teleportTo;
    private Teleporter old;
    float axis9 = 0;

    private void Start()
    {
        black = new Texture2D(1, 1);
        black.SetPixel(0, 0, new Color(0, 0, 0, 0));
        black.Apply();
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
    }

    void Update()
    {
        if (direction != 0)
        {
            alpha += direction;

            black.SetPixel(0, 0, new Color(0, 0, 0, alpha));
            black.Apply();

            if (alpha >= 1f)
            {
                direction = -direction;

                transform.SetParent(teleportTo);
                transform.localPosition = Vector3.forward * (transform.position.y - teleportTo.position.y);
                transform.SetParent(null);

                //var tPos = teleportTo;
                //var offset = transform.position.y + Camera.main.transform.position.y;
                //var collider = GetComponentInChildren<BoxCollider>();
                //transform.position = new Vector3(tPos.x, transform.position.y, tPos.y);
                //transform.position = new Vector3(tPos.x - collider.transform.localPosition.x, transform.position.y, tPos.z - collider.transform.localPosition.z);
            }
            else if (alpha <= 0f)
            {
                direction = 0f;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var teleporter = GetClosestTeleporter();

        if (teleporter)
        {

            teleporter.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 66 / 255f, 113 / 255f));
            if (!teleporter.particles.isPlaying) teleporter.particles.Play();

            if ((Input.GetKey(KeyCode.T) || Input.GetAxis("Axis9") > 0) && axis9 == 0)
            {
                teleportTo = teleporter.transform;
                teleporter.GetComponent<AudioSource>().Play();
                direction = Time.deltaTime * teleportSpeed;
                teleporter.particles.Stop();
            }
        }

        if (teleporter != old && old)
        {
            old.particles.Stop();
            old.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }

        old = teleporter;
        axis9 = Input.GetAxis("Axis9") + (Input.GetKey(KeyCode.T) ? 1:0);
    }

    Teleporter GetClosestTeleporter()
    {
        Teleporter closest = null;
        float minDistance = -1;

        var radius = GetComponentInChildren<SphereCollider>();
        var cam = Camera.main.transform;

        foreach (var teleporter in GameObject.FindGameObjectsWithTag("Teleporter"))
        {
            var teleport = teleporter.GetComponent<Teleporter>();
            if (!teleport || !teleport.isEnabled || !InSight(teleporter)) continue;

            Debug.DrawRay(cam.position, teleporter.transform.position - cam.position, Color.red);

            RaycastHit hit;
            Physics.Raycast(cam.position, teleporter.transform.position - cam.position, out hit);

            var other = hit.collider.gameObject;
            Debug.Log(hit.collider.name + ", " + (other == teleporter));
            if (other != teleporter) continue;


            Debug.DrawRay(cam.position, teleporter.transform.position - cam.position, Color.green);
            var dir = teleport.transform.position - cam.position;
            var angularDist = Vector3.Angle(dir, cam.forward);


            if (angularDist < minDistance || minDistance < 0)
            {
                minDistance = angularDist;
                closest = teleport;
            }
        }

        return closest;
    }

    bool InSight(GameObject teleporter)
    {
        Vector3 v = Camera.main.WorldToViewportPoint(teleporter.transform.position);
        if (!(v.x >= 0 && v.x <= 1 && v.y >= 0 && v.y <= 1 && v.z > 0))
            return false;
        else 
            return true;
    }
}
