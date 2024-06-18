using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blink : Ability
{   
    private void Awake() 
    {
        manaCost = 20;
        cooldownDuration = 2f;
    }

    protected override void PerformAbility()
    {
        Debug.Log("Blink performed");
    }
}
