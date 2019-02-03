using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagnetManager : MonoBehaviour
{
    private static Dictionary<Pole, Color> colors = new Dictionary<Pole, Color>
    {
        [Pole.NORTH] = Color.red,
        [Pole.SOUTH] = Color.blue
    };

    public static float magnetizeTime = 5f;
    public List<MagnetObject> objects = new List<MagnetObject>();

    public float distanceDecay = 1f;
    public float magnetStrength = 10f;

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            var body = objects[i].obj.GetComponent<Rigidbody>();
            if (body && !objects[i].obj.CompareTag("Still"))
            {
                var force = CalculateForce(i);
                body.AddForce(force * magnetStrength * objects[i].strength);
            }

            if (UpdateTime(i)) i--;
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

            float magnitude = Mathf.Max(0.1f, difference.magnitude) * distanceDecay;
            force += difference * otherObject.strength / magnitude;
        }
        return force;
    }

    bool UpdateTime(int i)
    {
        objects[i].time -= Time.fixedDeltaTime;

        Color magnetColor = colors[objects[i].pole];
        Color lerpColor = Color.Lerp(objects[i].originalColor, magnetColor, objects[i].time / magnetizeTime);
        objects[i].renderer.material.color = lerpColor;

        if (objects[i].time <= 0)
        {
            objects.RemoveAt(i);
            return true;
        }

        return false;
    }
}
