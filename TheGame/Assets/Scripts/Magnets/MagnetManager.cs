using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagnetManager : MonoBehaviour
{
    private static Dictionary<Pole, Color> colors = new Dictionary<Pole, Color>
    {
        [Pole.NORTH] = new Color(9 / 255f, 132 / 255f, 227 / 255f),
        [Pole.SOUTH] = new Color(192 / 255f, 57 / 255f, 43 / 255f)
    };

    public static float magnetStrength = 50f;
    public static float magnetizeTime = 5f;
    public static float maxStrength = 100f;
    public static float strengthCompound = 1.1f;

    public List<MagnetObject> objects = new List<MagnetObject>();

    public float distanceDecay = 0.01f;

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            try
            {
                var body = objects[i].obj.GetComponent<Rigidbody>();
                if (body && !objects[i].obj.CompareTag("Still"))
                {
                    var force = CalculateForce(i);
                    body.AddForce(force * magnetStrength * objects[i].strength);
                }

                if (UpdateTime(i)) i--;
            }
            catch (MissingReferenceException e)
            {
                objects.RemoveAt(i);
                i--;
            }
        }        
    }

    Vector3 CalculateForce(int i)
    {
        var position = objects[i].obj.transform.position;
        Vector3 force = new Vector3(0, 0, 0);
        foreach (var otherObject in objects)
        {
            if (ReferenceEquals(otherObject, objects[i])) continue;

            var closestPoint = otherObject.obj.GetComponent<Collider>().ClosestPoint(position);
            Vector3 difference = closestPoint - position;
            if (otherObject.pole == objects[i].pole) difference = -difference;

            float magnitude = Mathf.Log(Mathf.Max(1f, difference.magnitude) + 10) * distanceDecay;
            force += difference.normalized * otherObject.strength / magnitude;
        }
        Debug.Log(force);
        return force;
    }

    bool UpdateTime(int i)
    {
        objects[i].time -= Time.fixedDeltaTime;

        Color magnetColor = colors[objects[i].pole];
        Color lerpColor = Color.Lerp(objects[i].originalColor, magnetColor, objects[i].time / magnetizeTime);
        if(objects[i].renderer) objects[i].renderer.material.color = lerpColor;

        if (objects[i].time <= 0)
        {
            objects.RemoveAt(i);
            return true;
        }

        return false;
    }
}
