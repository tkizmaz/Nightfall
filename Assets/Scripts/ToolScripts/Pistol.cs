using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        weaponType = WeaponType.Pistol;
    }

    public override void PerformAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 20))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red, 1f);
            Debug.Log("Hit: " + hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                CombatManager.instance.DealDamage(hit.transform.GetComponent<Enemy>(), 10);
            }
        }
    }
}
