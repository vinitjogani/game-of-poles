using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var minAngle = 360f;
        GameObject minLevel = null;
        foreach (var level in GameObject.FindGameObjectsWithTag("Button"))
        {
            Vector3 dir = level.transform.position - Camera.main.transform.position;
            var angle = Vector3.Angle(dir, Camera.main.transform.forward);
            if (angle < minAngle)
            {
                minAngle = angle;
                minLevel = level;
            }
        }

        if (minLevel != null)
            minLevel.GetComponent<Button>().Select();

        if (Input.GetAxis("Axis9") > 0 || Input.GetKey(KeyCode.E))
        {
            if (minLevel.name == "Quit")
                Application.Quit();
            else
                Application.LoadLevel(minLevel.name);
        }
    }
}
