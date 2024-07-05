using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Stat
{
    protected override void Awake() 
    {
        base.Awake();
        statType = StatType.Health;    
    }
}
