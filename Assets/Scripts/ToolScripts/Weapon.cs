using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
    Pistol
}

public class Weapon : MonoBehaviour
{
    public PlayerAnimationController playerAnimationController;
    protected WeaponType weaponType;
    public WeaponType WeaponType => weaponType;

    private void Start() 
    {
    }

    public virtual void PerformAttack() 
    {
        Debug.Log("Weapon Attack");
    }

    public virtual void ActivateWeapon() 
    {
        gameObject.SetActive(true);
    }
    
    public virtual void DeactivateWeapon() 
    {
        gameObject.SetActive(false);
    }
}
