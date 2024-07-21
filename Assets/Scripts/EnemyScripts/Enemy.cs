using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public enum EnemyState
{
    Alerted,
    Idle,
    Patrol,
    Chase,
    Attack,
    Dead,
    Stunned
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
    protected NavMeshAgent navMeshAgent;
    private Animator enemyAnimator;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public bool IsEnemyAlerted{ get;}
    bool hasPlayerSeen = false;
    private bool isAttackFinished = false;
    private Rigidbody enemyBody;
    public EnemyState EnemyState => enemyState;
    [SerializeField]
    protected EnemyWeapon enemyWeapon;
    private float attackDelay = 0.65f;
    protected float attackRange;

    protected void Awake() 
    {
        health = this.gameObject.AddComponent<Health>();
        health.onStatFinished.AddListener(KillBySwordFight);
        enemyBody = this.GetComponent<Rigidbody>();
    }

    protected void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        SetEnemyPatrol();
    }

    protected void Update()
    {
        if(enemyState != EnemyState.Dead && player.gameObject.GetComponent<Player>().HealthState == HealthState.Alive && enemyState != EnemyState.Stunned)
        {
            CheckPlayerInSight();
            CheckIfPlayerDisappeared();
            CheckPatrolDestinationReached();
        }
        else if(enemyState != EnemyState.Dead && player.gameObject.GetComponent<Player>().HealthState == HealthState.Dead && enemyState != EnemyState.Patrol)
        {
            enemyState = EnemyState.Patrol;
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
                    if(!hasPlayerSeen)
                    {
                        CheckForPlayerSeen();
                    }

                    bool isPlayerInAttackRange = distanceToPlayer <= attackRange;
                    if(isPlayerInAttackRange)
                    {
                        if(enemyState != EnemyState.Attack)
                        {
                            enemyState = EnemyState.Attack;
                            StartCoroutine(SetEnemyToAttackState());
                        }
                    }
                    else
                    {
                        enemyAnimator.SetBool("onStance", false);
                        navMeshAgent.SetDestination(player.position);
                        SetEnemyToChasingState();
                    }
                }
                else
                {
                    SetEnemyPatrol();
                }
            }

            else if(player.GetComponent<PlayerMovement>().PlayerState != PlayerState.Idle && player.GetComponent<PlayerMovement>().PlayerState != PlayerState.Walking)
            {
                if(!hasPlayerSeen)
                {
                    CheckForPlayerSeen();
                }
                enemyAnimator.SetBool("onStance", false);                
                navMeshAgent.SetDestination(player.position);
                SetEnemyToChasingState();
            }
        }
    }

    protected virtual void SetEnemyToChasingState()
    {
        enemyState = EnemyState.Chase;
        enemyAnimator.SetBool("isRunning", true);
        navMeshAgent.speed = 4f;
    }

    void CheckIfPlayerDisappeared()
    {
        if(enemyState == EnemyState.Chase)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if(distanceToPlayer > visionRange)
            {
                AudioManager.instance.PlayCowardSfx();
                SetEnemyPatrol();
                enemyAnimator.SetBool("isRunning", false);
                hasPlayerSeen = false;
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

    private void KillBySwordFight()
    {
        SetStateToDeath();
        enemyAnimator.SetTrigger("Death");    
    }

    public void SetStateToDeath()
    {
        enemyState = EnemyState.Dead;
        navMeshAgent.isStopped = true;
        enemyBody.isKinematic = true;
        //enemySword.SetSwordCollider(false);
    }

    protected virtual IEnumerator SetEnemyToAttackState()
    {
        navMeshAgent.isStopped = true;
        while (enemyState == EnemyState.Attack)
        {
            string animation = SelectAttackAnimation();
            enemyAnimator.SetBool("onStance", true);

            isAttackFinished = false;
            enemyAnimator.SetTrigger(animation);
            enemyWeapon.DeactivateWeapon(true);

            yield return new WaitUntil(() => isAttackFinished);
            enemyWeapon.DeactivateWeapon(false);

            if(enemyState != EnemyState.Attack)
            {
                navMeshAgent.isStopped = false;
                break;
            }
            yield return new WaitForSeconds(attackDelay);
        }
        navMeshAgent.isStopped = false;
    }

    private void CheckForPlayerSeen()
    {
        hasPlayerSeen = true;
        AudioManager.instance.PlayPlayerDetectedSfx();
    }
    

    public void SetAttackFinished()
    {
        isAttackFinished = true;
    }

    public void PlayKickFinisherEnemy()
    {
        SetStateToDeath();
        enemyAnimator.SetTrigger("KickFinisher");
    }

    public void OnTakeDamage()
    {
        StartCoroutine(ApplyStunnedState());
    }

    private IEnumerator ApplyStunnedState()
    {
        if(enemyState == EnemyState.Attack)
        {
            StopCoroutine(SetEnemyToAttackState());
            enemyWeapon.DeactivateWeapon(false);
            enemyState = EnemyState.Stunned;
            enemyAnimator.SetTrigger("Impact");
            navMeshAgent.isStopped = true;
            yield return new WaitUntil(() => enemyState != EnemyState.Stunned);
            navMeshAgent.isStopped = false;
        }
        else
        {
            enemyAnimator.SetTrigger("Impact");
        }

    }

    public void SetStunnedStateFinished()
    {
        enemyState = EnemyState.Idle;
    }

    protected virtual string SelectAttackAnimation()
    {
        return "";
    }
}