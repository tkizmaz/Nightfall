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
    private Health health;
    public Health Health => health;
    private EnemyState enemyState = EnemyState.Idle;
    public float visionRange = 10.0f;
    public float visionAngle = 60.0f;
    public LayerMask obstacleMask;
    public Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator enemyAnimator;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public bool IsEnemyAlerted{ get;}
    [SerializeField]
    private float attackDelay = 3f;
    string SLASH_ANIMATION = "Slash";
    string SLASH_ANIMATION_2 = "ShieldSlash";

    private void Awake() 
    {
        health = this.gameObject.AddComponent<Health>();
        health.onStatFinished.AddListener(SetStateToDeath);
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        SetEnemyPatrol();
    }

    void Update()
    {
        if(enemyState != EnemyState.Dead)
        {
            CheckPlayerInSight();
            CheckIfPlayerDisappeared();
            CheckPatrolDestinationReached();
        }
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
                    bool isPlayerInAttackRange = distanceToPlayer <= 3.0f;
                    if(isPlayerInAttackRange && enemyState != EnemyState.Attack)
                    {
                        enemyState = EnemyState.Attack;
                        StartCoroutine(SetEnemyToAttackState());
                    }
                    else
                    {
                        enemyAnimator.SetBool("onStance", false);
                        SetEnemyToChasingState();
                    }
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
        navMeshAgent.speed = 4f;
        navMeshAgent.stoppingDistance = 3.0f;
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
        navMeshAgent.speed = 1f;
        navMeshAgent.stoppingDistance = 0.0f;
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
    }

    public void SetStateToDeath()
    {
        enemyState = EnemyState.Dead;
        navMeshAgent.enabled = false;
        enemyAnimator.SetTrigger("Death");
    }

    private IEnumerator SetEnemyToAttackState()
    {
        while(enemyState == EnemyState.Attack)
        {
            //Randomize the attack animation
            int randomSlash = Random.Range(0, 2);
            string slashAnimation = randomSlash == 0 ? SLASH_ANIMATION : SLASH_ANIMATION_2;
            enemyAnimator.SetTrigger(slashAnimation);
            yield return new WaitForSeconds(attackDelay);
            enemyAnimator.SetBool("onStance", true);
            
        }
    }

}
