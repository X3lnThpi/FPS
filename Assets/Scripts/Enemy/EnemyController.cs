using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody theRB;
    private bool chasing = false;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;

    private Vector3 targetpoint, startPoint;
    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    private void Start()
    {
        startPoint = transform.position;
    }

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

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                }
            }
        }
        else
        {
            //transform.LookAt(targetpoint);

            //theRB.velocity = transform.forward * moveSpeed;

            if(Vector3.Distance(transform.position, targetpoint) > distanceToStop)
            {
                agent.destination = targetpoint;
            }
            else
            {
                agent.destination = transform.position;
            }
            

            if (Vector3.Distance(transform.position, targetpoint) > distanceToLose)
            {
                chasing = false; Debug.Log("2 ...Distance is" + Vector3.Distance(transform.position, targetpoint));
                chaseCounter = keepChasingTime;
                agent.destination = startPoint;
            }
        }
        } 
        
    }

 