using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3f;
    public float visionRadius = 4f;
    public float chaseDuration = 5f; 
    float returnPatrolDuration = 5f;

    private Transform player;
    private int currentPatrolIndex = 0;
    private float timer;
    bool canRun = true, returnToPatrol = false;
    private Animator animator;

    private void Awake ()
    {
        gameObject.transform.DetachChildren();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = chaseDuration;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //if (PauseMenu.instancePausa.isPaused == false) {
        if (returnToPatrol == false)
        {
            if (CanSeePlayer() == true)
            {
                ChasePlayer();
            } else
            {
                Patrol();
            }
        } else if (returnToPatrol == true)
        {
            Patrol();

            if (returnPatrolDuration > 0f)
            {
                returnPatrolDuration -= Time.deltaTime;
            } else
            {
                returnPatrolDuration = 5f;
                returnToPatrol = false;
            }
        } 
        //}
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
        if (canRun == true) {
            timer -= Time.deltaTime;
            Vector2 direction = player.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            SetAnimationParameters(direction);

            if (timer <= 0f)
            {
                timer = chaseDuration;
                returnToPatrol = true;
            }
        }
    }

    void Patrol()
    {
        if (canRun == true) {
            Vector2 direction = patrolPoints[currentPatrolIndex].position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, patrolSpeed * Time.deltaTime);
            SetAnimationParameters(direction);

            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.2f)
            {
                if (currentPatrolIndex == patrolPoints.Length - 1)
                {
                    currentPatrolIndex = 0;
                } else
                {
                    currentPatrolIndex++;
                }

                UnityEngine.Debug.Log(gameObject.name + " ha cambiado el destino a " + patrolPoints [currentPatrolIndex].ToString());
            }
        }
    }

    void SetAnimationParameters(Vector2 directionEnemy)
    {
        if (Mathf.Abs(directionEnemy.x) > Mathf.Abs(directionEnemy.y))
        {
            animator.SetFloat("Horizontal", directionEnemy.x);
            animator.SetFloat("Vertical", 0f);
        } else
        {
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", directionEnemy.y);
        }
    }

    public void StunEnemy () {
        float y = 2f;

        LeanTween.value(y, 0f, 2f).setOnComplete(()=> {
            canRun = true;
        });
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.tag == "Poop") {
            canRun = false;
        }
    }
}