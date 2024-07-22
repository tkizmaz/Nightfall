using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField]
    private Transform pistolTip;
    void Start()
    {
        weaponType = WeaponType.Pistol;
    }

    public override void ActivateWeapon()
    {
        Debug.Log("Pistol Activated");
        base.ActivateWeapon();
    }

    public override void PerformAttack()
    {
        playerAnimationController.PlayShootAnimation();
        RaycastHit hit;
        if (Physics.Raycast(pistolTip.transform.position, pistolTip.transform.forward, out hit, 20))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red, 1f);
            if (hit.transform.CompareTag("Enemy"))
            {
                CombatManager.instance.DealDamage(hit.transform.GetComponent<Enemy>(), 10);
            }
        }
    }
}
