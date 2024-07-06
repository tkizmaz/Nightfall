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
        if(Input.GetMouseButtonDown(0))
        {
            // Raycast işlemi
            RaycastHit hit;
            Vector3 rayOrigin = Camera.main.transform.position;
            Vector3 rayDirection = Camera.main.transform.forward;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, finisherRange))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Finisher");
                    return;
                }
            }
            else
            {
                AudioManager.instance.PlaySwordSwingSfx();
                swordCollider.enabled = true;
                onPlayerSlash.Invoke();
            
            }
        }
    }
}
