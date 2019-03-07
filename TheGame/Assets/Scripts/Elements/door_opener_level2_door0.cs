using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_opener_level2_door0 : MonoBehaviour
{
    Animator anim;
    BoxCollider boxColl;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider>();
    }

  

    private void OnCollisionEnter(Collision collision)
    {
        anim.SetTrigger("character_nearby");
        // Play door sound
        AudioSource laudio = gameObject.AddComponent<AudioSource>();
        laudio.PlayOneShot((AudioClip)Resources.Load("fallingWall"));
        boxColl.enabled = false;
    }
}
