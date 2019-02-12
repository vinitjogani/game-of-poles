using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAudio : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying) Destroy(gameObject);
    }

}
