using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected float cooldownDuration;
    protected int manaCost;
    public int ManaCost
    {
        get { return manaCost; }
        set { manaCost = value; }
    }

    public float CooldownDuration
    {
        get { return cooldownDuration; }
        set { cooldownDuration = value; }
    }

    protected virtual void PerformAbility()
    {
        Debug.Log("Ability performed");
    }
}
