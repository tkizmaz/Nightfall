using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    string SHOOT_ANIMATION = "Shoot";

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        attackRange = 7.0f;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    protected new void Awake()
    {
        base.Awake();    
    }

    protected override void SetEnemyToChasingState()
    {
        base.SetEnemyToChasingState();
        navMeshAgent.stoppingDistance = 7.0f;
    }

    protected override string SelectAttackAnimation()
    {
        return SHOOT_ANIMATION;
    }

    public void Attack()
    {
        EnemyPistol enemyPistol = enemyWeapon as EnemyPistol;
        enemyPistol.Shoot();
    }
}
