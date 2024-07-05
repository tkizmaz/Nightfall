using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum EnemyState
{
    Alerted,
    Idle,
    Patrol,
    Chase,
    Attack,
    Dead
}

public class Enemy : MonoBehaviour
{
    private EnemyState enemyState = EnemyState.Idle;
    public float visionRange = 10.0f;
    public float visionAngle = 60.0f;
    public LayerMask obstacleMask;
    public Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator enemyAnimator;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private bool isEnemyAlerted = false;
    public bool IsEnemyAlerted{ get;}

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        SetEnemyPatrol();
    }

    void Update()
    {
        CheckPlayerInSight();
        CheckIfPlayerDisappeared();
        CheckPatrolDestinationReached();
    }

    void CheckPlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= visionRange)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= visionAngle / 2)
            {
                if (!Physics.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer, obstacleMask))
                {
                    SetEnemyToChasingState();
                    navMeshAgent.SetDestination(player.position);
                }
                else
                {
                    enemyState = EnemyState.Patrol;
                }
            }
        }
    }

    private void SetEnemyToChasingState()
    {
        enemyState = EnemyState.Chase;
        enemyAnimator.SetBool("isRunning", true);
        navMeshAgent.speed = 5f;
        isEnemyAlerted = true;
    }

    void CheckIfPlayerDisappeared()
    {
        if(enemyState == EnemyState.Chase)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if(distanceToPlayer > visionRange)
            {
                SetEnemyPatrol();
                enemyAnimator.SetBool("isRunning", false);
            }
        }
    }

    void SetEnemyPatrol()
    {
        enemyState = EnemyState.Patrol;
        isEnemyAlerted = false;
        navMeshAgent.speed = 1f;
        enemyAnimator.SetBool("isPatrolling", true);
        SetPatrolDestination();
    }

    void SetPatrolDestination()
    {
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    void CheckPatrolDestinationReached()
    {
        if(enemyState == EnemyState.Patrol)
        {
            if(Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 1.0f)
            {
                currentPatrolIndex++;
                if(currentPatrolIndex >= patrolPoints.Length)
                {
                    currentPatrolIndex = 0;
                }
                SetPatrolDestination();
            }
        }
        else
        {
            Debug.Log("Not in patrol state");
        }
    }

    public void SetStateToDeath()
    {
        enemyState = EnemyState.Dead;
        navMeshAgent.enabled = false;
        enemyAnimator.SetTrigger("Death");
    }

}
