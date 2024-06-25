using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendTime : Ability
{
    private float slowDownDuration = 5f;
    private void Awake() 
    {
        abilityType = AbilityType.BendTime;
        manaCost = 50;
        cooldownDuration = 5f;
    }

    private void Update() 
    {
        PerformAbility();
    }
    
    protected override void PerformAbility()
    {
        bool hasEnoughMana = HasEnoughMana();
        if(isReadyToPerform && hasEnoughMana && isAbilitySelected)
        {
            if(Input.GetMouseButtonDown(1))
            {
                StartCoroutine(SlowDownTime());
                base.PerformAbility();
            }
        }
    }

    private IEnumerator SlowDownTime()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(slowDownDuration);
        Time.timeScale = 1f;
    } 
}
