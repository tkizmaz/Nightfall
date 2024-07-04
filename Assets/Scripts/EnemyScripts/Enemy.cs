using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public GameObject player;

    [SerializeField]
    private NavMeshAgent navMeshAgent;
    private Health health;
    
    void Start()
    {
        health = GetComponent<Health>();
    }

    private void Awake() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() 
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        if(Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
        {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);
            if(!Physics.Raycast(transform.position, directionToPlayer, playerDistance, obstacleMask))
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
        }

    }
}
