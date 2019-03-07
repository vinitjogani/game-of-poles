using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class CollisionSound : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        AudioSource laudio = gameObject.AddComponent<AudioSource>();
        laudio.volume = 1 - 1 / collision.relativeVelocity.sqrMagnitude;
        int rand = UnityEngine.Random.Range(0, 3);
        laudio.PlayOneShot((AudioClip)Resources.Load("collision{rand}"));
    }
}
