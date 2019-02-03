using UnityEngine;

public class MagnetObject
{
    public readonly GameObject obj;
    public Pole pole;
    public float time;
    public float strength;
    public Color originalColor;
    public Renderer renderer;

    public MagnetObject(GameObject obj, Pole pole, float time, float strength = 1f)
    {
        this.obj = obj;
        this.pole = pole;
        this.time = time;
        this.strength = strength;

        renderer = obj.GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }
}