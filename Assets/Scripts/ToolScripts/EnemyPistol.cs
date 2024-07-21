using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPistol : EnemyWeapon
{
    private Transform pistolTip;

    void Start()
    {
        pistolTip = this.gameObject.transform.GetChild(0);
    }

    public void Shoot()
    {
        enemySoundManager.PlayPistolShootSound();
        RaycastHit hit;
        if (Physics.Raycast(pistolTip.position, pistolTip.forward, out hit, 100))
        {   
            Debug.DrawRay(pistolTip.position, pistolTip.forward * hit.distance, Color.red, 100f);
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                CombatManager.instance.DealDamageToPlayer(25);
            }
        }
    }
    
}
