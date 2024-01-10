using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float visionRadius = 5f;
    public float chaseDuration = 5f;

    private Transform player;
    private int currentPatrolIndex = 0;
    private float timer;

    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = chaseDuration;
        animator = GetComponent<Animator>();
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
            SetAnimationParameters(player.position - transform.position);
            return true;
        }

        return false;
    }

    void ChasePlayer()
    {
        timer -= Time.deltaTime;
        Vector2 direction = player.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        SetAnimationParameters(direction);

        if (timer <= 0f)
        {
            timer = chaseDuration;
            Patrol();
        }
    }

    void Patrol()
    {
        Vector2 direction = patrolPoints[currentPatrolIndex].position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, patrolSpeed * Time.deltaTime);
        SetAnimationParameters(direction);

        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void SetAnimationParameters(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        animator.SetFloat("Direction", angle);
    }
}