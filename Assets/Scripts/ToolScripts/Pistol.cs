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
        base.ActivateWeapon();
    }

    public override void PerformAttack()
    {
        playerAnimationController.PlayShootAnimation();
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(pistolTip.position, pistolTip.forward, out hit, 100))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                CombatManager.instance.DealDamage(hit.transform.gameObject.GetComponent<Enemy>(), 25);
            }
        }
    }
}
