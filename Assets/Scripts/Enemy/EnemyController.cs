using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
 
    private bool chasing = false;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;

    private Vector3 targetpoint, startPoint;
    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    private void Start()
    {
        startPoint = transform.position;
        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
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
                chasing = true;
                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
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

            if(shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;

                if(shotWaitCounter < 0)
                {
                    shootTimeCounter = timeToShoot;
                }
            }
            else
            {
                shootTimeCounter -= Time.deltaTime;

                if (shootTimeCounter > 0)
                {
                    fireCount -= Time.deltaTime;

                    if (fireCount <= 0)
                    {
                        fireCount = fireRate;

                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                    }
                    agent.destination = transform.position;
                }
                else
                {
                    shotWaitCounter = waitBetweenShots;
                }
            }

        }
        } 
        
    }

 