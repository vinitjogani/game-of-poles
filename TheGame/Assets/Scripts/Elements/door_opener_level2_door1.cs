using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_opener_level2_door1 : MonoBehaviour
{
    Animator animat;
    // Start is called before the first frame update
    void Start()
    {
        animat = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animat.SetTrigger("character_nearby");
            AudioSource laudio = gameObject.AddComponent<AudioSource>();
            laudio.PlayOneShot((AudioClip)Resources.Load("fallingWall"));
        }

    }


}
