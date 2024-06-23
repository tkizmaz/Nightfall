using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManaPotion : Potion
{
    protected override void Start() 
    {
        base.Start();
        restoreValue = 30;
        statType = StatType.Mana;
    }
}
