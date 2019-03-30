using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent nav;
    private Actions actions;
    public float minMoveDistance = 5f;
    public float maxMoveDistance = 30f; 

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;

        actions = GetComponent<Actions>();
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;

        var head = transform.position + Vector3.up * 3;
        var distance = (Camera.main.transform.position - transform.position).magnitude;

        if (distance > minMoveDistance && distance < maxMoveDistance)
        {
            var enemy = GetComponent<EnemyAI>();
            if ((enemy.shotTime > 1f && enemy.shotTime < enemy.shotInterval) || enemy.maximumAttackDistance <= distance)
            {
                actions.Walk();
                nav.isStopped = false;
                nav.destination = Camera.main.transform.position;

                // Play walk sound
                AudioSource temp = GetComponent<AudioSource>();
                AudioSource laudio = temp ? temp : gameObject.AddComponent<AudioSource>();
                AudioClip audioClip = (AudioClip)Resources.Load("EnemyWalk");
                if (!laudio.isPlaying)
                {
                    laudio.volume = 0.2f;
                    laudio.PlayOneShot(audioClip);
                }
            }
            else
            {
                nav.isStopped = true;
            }
        }
        else
        {
            nav.isStopped = true;
            actions.Aiming();
        }
    }
}
