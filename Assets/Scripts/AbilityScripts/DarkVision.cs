using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkVision : Ability
{
    private float darkVisionDuration = 10f;
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
        List<GameObject> enemies = GameManager.instance.EnemyList;
        foreach(GameObject enemy in enemies)
        {
            ShaderController shaderController = enemy.GetComponent<ShaderController>();
            shaderController.ChangeToSilhouette();
        }
        yield return new WaitForSeconds(darkVisionDuration);
        foreach(GameObject enemy in enemies)
        {
            ShaderController shaderController = enemy.GetComponent<ShaderController>();
            shaderController.ChangeToDefault();
        }
    }
}
