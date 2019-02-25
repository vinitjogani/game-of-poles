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
        actions = GetComponent<Actions>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        var head = transform.position + Vector3.up * 2;

        Debug.DrawRay(head, transform.forward * 100f, Color.green);
        if (Physics.Raycast(head, transform.forward * 1000f, out hit))
        {
            if (hit.collider.CompareTag("Player") && hit.distance > 5f)
            {
                var enemy = GetComponent<EnemyAI>();
                if (enemy.shotTime > 1f && enemy.shotTime < enemy.shotInterval)
                {
                    actions.Walk();
                    nav.isStopped = false;
                    nav.destination = Camera.main.transform.position;
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
}
