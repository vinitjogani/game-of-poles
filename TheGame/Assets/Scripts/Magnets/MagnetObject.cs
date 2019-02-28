using System.Collections.Generic;
using UnityEngine;

public class MagnetObject
{
    public readonly GameObject obj;
    public Pole pole;
    public float time;
    public float strength;
    public List<Color> originalColor = new List<Color>();

    public MagnetObject(GameObject obj, Pole pole, float time, float strength = 1f)
    {
        this.obj = obj;
        this.pole = pole;
        this.time = time;
        this.strength = strength;

        foreach (var renderer in obj.GetComponentsInChildren<Renderer>())
        {
            try
            {
                originalColor.Add(renderer.material.color);
            }
            catch { }
        }
    }
}