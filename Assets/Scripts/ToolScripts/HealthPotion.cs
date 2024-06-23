using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    protected override void Start() 
    {
        restoreValue = 50;
        statType = StatType.Health;
    }
}
