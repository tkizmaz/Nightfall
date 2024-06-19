using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : Stat
{
    protected override void OnStatFinished()
    {
        Debug.Log("Mana finished");
    }

    public void OnAbilityUsed(int manaCost)
    {
        StatValue -= manaCost;
    }
}
