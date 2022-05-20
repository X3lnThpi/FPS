using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody theRB;
    private bool chasing = false;
    public float distanceToChase = 10f, distanceToLose = 15f;

    private Vector3 targetpoint;
    public NavMeshAgent agent;
    

    void Update()
    {
        targetpoint = PlayerController.instance.transform.position;
        targetpoint.y = transform.position.y;
        //Debug.Log("Target Point is :" + targetpoint);

        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetpoint) < distanceToChase)
            {
                chasing = true; Debug.Log("Distance is" + Vector3.Distance(transform.position, targetpoint));
            }
        }
        else
        {
            //transform.LookAt(targetpoint);

            //theRB.velocity = transform.forward * moveSpeed;

            agent.destination = targetpoint;

            if (Vector3.Distance(transform.position, targetpoint) > distanceToLose)
            {
                chasing = false; Debug.Log("2 ...Distance is" + Vector3.Distance(transform.position, targetpoint));
            }
        }
        } 
        
    }

 