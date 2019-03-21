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
        AudioSource temp = GetComponent<AudioSource>();
        AudioSource laudio = temp ? temp : gameObject.AddComponent<AudioSource>();
        laudio.volume = 1 - 1 / collision.relativeVelocity.magnitude;
        laudio.pitch = UnityEngine.Random.Range(-1.0f, 2.0f);
        int rand = UnityEngine.Random.Range(1, 4);
        if (!laudio.isPlaying) laudio.PlayOneShot((AudioClip)Resources.Load("collision"+rand));

    }
}
