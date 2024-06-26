using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkVision : Ability
{
    private void Awake() 
    {
        abilityType = AbilityType.DarkVision;
        manaCost = 30;
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
                StartCoroutine(ChangeShaders());
                base.PerformAbility();
            }
        }
    }
    
    public IEnumerator ChangeShaders()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            CubeController cubeController = enemy.GetComponent<CubeController>();
            cubeController.ChangeToSilhouette();
        }
        yield return new WaitForSeconds(10f);
        foreach(GameObject enemy in enemies)
        {
            CubeController cubeController = enemy.GetComponent<CubeController>();
            cubeController.ChangeToDefault();
        }
    }
}
