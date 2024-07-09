using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    private BoxCollider swordCollider;
    [SerializeField]
    private EnemySoundManager enemySoundManager;
    // Start is called before the first frame update
    void Start()
    {
        swordCollider = this.gameObject.GetComponent<BoxCollider>();
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemySoundManager.PlaySwordHitPlayerSfx();
            CombatManager.instance.DealDamageToPlayer(20);
            swordCollider.enabled = false;
        }
    }

    public void SetSwordCollider(bool value)
    {
        swordCollider.enabled = value;
    }
}
