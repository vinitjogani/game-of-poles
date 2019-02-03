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

                var tPos = teleportTo.position;
                transform.position = new Vector3(tPos.x, tPos.y + transform.localScale.y / 2, tPos.z);
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
        if (Input.GetKey(KeyCode.T))
        {
            var teleporter = GetClosestTeleporter();
            if (teleporter)
            {
                teleportTo = teleporter.transform;
                teleporter.GetComponent<AudioSource>().Play();
                direction = Time.deltaTime * teleportSpeed;
            }
        }
    }

    GameObject GetClosestTeleporter()
    {
        GameObject closest = null;
        float minDistance = -1;

        foreach (var teleporter in GameObject.FindGameObjectsWithTag("Teleporter"))
        {
            Vector3 v = Camera.main.WorldToViewportPoint(teleporter.transform.position);
            if (!(v.x >= 0 && v.x <= 1 && v.y >= 0 && v.y <= 1 && v.z > 0)) continue;

            if (teleporter.GetComponent<Teleporter>().isEnabled)
            {
                var point = teleporter.GetComponent<Collider>().ClosestPoint(transform.position);
                var distance = (point - transform.position).magnitude;
                if (minDistance == -1 || distance < minDistance)
                {
                    closest = teleporter;
                    minDistance = distance;
                }
            }
        }

        return closest;
    }
}
