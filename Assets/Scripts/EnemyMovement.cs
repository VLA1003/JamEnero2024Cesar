using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float visionRadius = 5f;
    public float chaseDuration = 5f;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private int currentPatrolIndex = 0;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        timer = chaseDuration;
        SetPatrolDestination();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    bool CanSeePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < visionRadius)
        {
            return true;
        }

        return false;
    }

    void ChasePlayer()
    {
        timer -= Time.deltaTime;
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(player.position);

        if (timer <= 0f)
        {
            timer = chaseDuration;
            SetPatrolDestination();
        }
    }

    void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            SetPatrolDestination();
        }
    }

    void SetPatrolDestination()
    {
        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
}
