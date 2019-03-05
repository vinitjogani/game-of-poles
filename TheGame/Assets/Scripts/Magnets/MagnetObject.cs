using System.Collections.Generic;
using UnityEngine;

public class MagnetObject: MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource laudio = gameObject.AddComponent<AudioSource>();
        laudio.volume = 1 - 1/collision.relativeVelocity.sqrMagnitude;
        int rand = Random.Range(0, 3);
        laudio.PlayOneShot((AudioClip)Resources.Load("collision{rand}"));
    }
}