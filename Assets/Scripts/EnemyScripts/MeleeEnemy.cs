using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    string SLASH_ANIMATION = "Slash";
    string SLASH_ANIMATION_2 = "ShieldSlash";
    private EnemySword enemySword;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        attackRange = 3.0f;
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
        navMeshAgent.stoppingDistance = 3.0f;
    }

    protected override string SelectAttackAnimation()
    {
        int randomSlash = Random.Range(0, 2);
        string slashAnimation = randomSlash == 0 ? SLASH_ANIMATION : SLASH_ANIMATION_2;
        return slashAnimation;
    }
}
