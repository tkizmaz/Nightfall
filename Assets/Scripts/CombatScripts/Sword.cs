using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    private BoxCollider swordCollider;
    public UnityEvent onPlayerSlash;
    public PlayerAnimationController playerAnimationController;
    private float finisherRange = 1f;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = this.gameObject.GetComponent<BoxCollider>();
        swordCollider.enabled = false;
        if(playerAnimationController != null)
        {
            onPlayerSlash.AddListener(playerAnimationController.PlaySlashAnimation);
        }
    }

    private void Update() 
    {
        CheckAttack();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.PlaySwordHitEnemySfx();
            CombatManager.instance.DealDamage(other.gameObject.GetComponent<Enemy>(), 30);
            StartCoroutine(DisableCollider());
        }
    }

    private IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        swordCollider.enabled = false;
    }

    private void CheckAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Vector3 rayOrigin = Camera.main.transform.position;
            Vector3 rayDirection = Camera.main.transform.forward;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, finisherRange))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Vector3 playerPosition = Camera.main.transform.position;
                    Vector3 enemyPosition = hit.collider.gameObject.transform.position;
                    Vector3 directionToPlayer = (playerPosition - enemyPosition).normalized;
                    Vector3 enemyForward = hit.collider.gameObject.transform.forward;
                    float dotProduct = Vector3.Dot(enemyForward, directionToPlayer);
                    if (dotProduct < -0.5f)
                    {
                        FinisherAnimationController.instance.PlayFinisherAnimation(playerAnimationController, hit.collider.gameObject.GetComponent<Enemy>());
                        return;
                    }
                }
            }
            
            // Düşman bulunamadı veya mesafe uygun değilse kılıç sesi çal ve saldırı yap
            AudioManager.instance.PlaySwordSwingSfx();
            swordCollider.enabled = true;
            onPlayerSlash.Invoke();
        }
    }

}
