using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent nav;
    private Actions actions;

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
        //Debug.DrawRay(head, transform.forward * distance + Vector3.down * 2, Color.green);
        //Ray ray = new Ray(head, transform.forward * distance + Vector3.down * 2);

        //if (Physics.Raycast(ray, out hit, float.MaxValue, ~0, QueryTriggerInteraction.Ignore))
        //{
            if (distance > 5f && distance < 30f)
            {
                var enemy = GetComponent<EnemyAI>();
                Debug.Log(enemy.shotTime + ", " + enemy.shotInterval);
                if (enemy.shotTime > 1f && enemy.shotTime < enemy.shotInterval)
                {
                    actions.Walk();
                    nav.isStopped = false;
                    nav.destination = Camera.main.transform.position;

                    // Play damage sound
                    AudioSource laudio = gameObject.AddComponent<AudioSource>();
                    laudio.PlayOneShot((AudioClip)Resources.Load("EnemyWalk"));
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
        //}
    }
}
