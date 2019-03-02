using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineKey : MonoBehaviour
{
    public GameObject NodeKey;
    private bool alreadyCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == NodeKey && !alreadyCollided)
        {
            // Play key destroyed sound
            AudioSource laudio = gameObject.AddComponent<AudioSource>();
            laudio.PlayOneShot((AudioClip)Resources.Load("KeyUnlocked"));

            alreadyCollided = true;
            GetComponentInParent<MachineController>().uniqueKeyCollided();
            Destroy(NodeKey);

        }
    }
}
