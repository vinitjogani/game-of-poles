using System.Collections.Generic;
using UnityEngine;

public class MagnetObject
{
    public readonly GameObject obj;
    public Pole pole;
    public float time;
    public float strength;
    public List<Color> originalColor = new List<Color>();
    public Renderer[] renders;

    public MagnetObject(GameObject obj, Pole pole, float time, float strength = 1f)
    {
        this.obj = obj;
        this.pole = pole;
        this.time = time;
        this.strength = strength;

        Debug.Log("Bullet hit " + obj.name);

        if (!obj.GetComponent<CollisionSound>()) obj.AddComponent<CollisionSound>();
        renders = obj.GetComponentsInChildren<Renderer>();

        foreach (var renderer in renders)
        {
            try
            {
                if (renderer.material.HasProperty("_Color"))
                    originalColor.Add(renderer.material.color);
                else originalColor.Add(Color.black);
            }
            catch { }
        }
    }
}