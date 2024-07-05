using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : Stat
{
    protected override void Awake() 
    {
        base.Awake();
        statType = StatType.Mana;    
    }

    public void OnAbilityUsed(int manaCost)
    {
        StatValue -= manaCost;
    }
}
