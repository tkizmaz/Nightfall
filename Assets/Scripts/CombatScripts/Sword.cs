using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum SwordState
{
    Idle,
    Attacking
}

public class Sword : Weapon
{
    private BoxCollider swordCollider;
    public UnityEvent onPlayerSlash;
    private float finisherRange = 1f;
    private SwordState swordState;

    // Start is called before the first frame update
    void Start()
    {
        weaponType = WeaponType.Sword;
        swordState = SwordState.Idle;
        swordCollider = this.gameObject.GetComponent<BoxCollider>();
        swordCollider.enabled = false;
        if(playerAnimationController != null)
        {
            onPlayerSlash.AddListener(playerAnimationController.PlaySlashAnimation);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.PlaySwordHitEnemySfx();
            CombatManager.instance.DealDamage(other.gameObject.GetComponent<Enemy>(), 30);
            swordCollider.enabled = false;
        }
    }

    private IEnumerator AttackRoutine()
    {
        AudioManager.instance.PlaySwordSwingSfx();
        swordState = SwordState.Attacking;
        swordCollider.enabled = true;
        onPlayerSlash.Invoke();

        yield return new WaitUntil(() => playerAnimationController.isAttackFinished);
        
        swordCollider.enabled = false;
        swordState = SwordState.Idle;
    }

    public override void PerformAttack()
    {
        Debug.Log("Sword Attack");
        if (swordState == SwordState.Idle)
        {
            RaycastHit hit;
            Vector3 rayOrigin = Camera.main.transform.position;
            Vector3 rayDirection = Camera.main.transform.forward;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, finisherRange) && hit.collider.gameObject.GetComponent<Enemy>().EnemyState == EnemyState.Patrol)
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Vector3 playerPosition = Camera.main.transform.position;
                    Vector3 enemyPosition = hit.collider.gameObject.transform.position;
                    Vector3 directionToPlayer = (playerPosition - enemyPosition).normalized;
                    Vector3 enemyForward = hit.collider.gameObject.transform.forward;
                    float dotProduct = Vector3.Dot(enemyForward, directionToPlayer);
                    if (dotProduct > -0.7f)
                    {
                        FinisherAnimationController.instance.PlayFinisherAnimation(playerAnimationController, hit.collider.gameObject.GetComponent<Enemy>());
                        return;
                    }
                }
            }
            
            StartCoroutine(AttackRoutine());

        }
    }
}
