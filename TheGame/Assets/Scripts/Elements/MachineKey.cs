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
            alreadyCollided = true;
            BroadcastMessage("uniqueKeyCollided");
            Destroy(NodeKey);
        }
    }
}
