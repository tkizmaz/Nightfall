using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class AbilityUsedEvent : UnityEvent<int> {}

[System.Serializable]
public class AbilityStatusEvent : UnityEvent<bool> {}

public class Ability : MonoBehaviour
{
    protected float cooldownDuration;
    protected int manaCost;
    protected bool isReadyToPerform = true;
    public AbilityUsedEvent abilityUsed;
    public AbilityStatusEvent isAbilityReady;
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
        isReadyToPerform = false;
        isAbilityReady.Invoke(isReadyToPerform);
        StartCooldown();
        abilityUsed.Invoke(manaCost);
    }

    protected virtual void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        isReadyToPerform = true;
        isAbilityReady.Invoke(isReadyToPerform);
    }

    private void Start() 
    {
        Mana playerMana = this.gameObject.GetComponent<Mana>();
        if(playerMana != null)
        {
            abilityUsed.AddListener(playerMana.OnAbilityUsed);
        }
        GameUI gameUI = GameObject.FindWithTag("GameController").GetComponent<GameUI>();
        if(gameUI != null)
        {
            isAbilityReady.AddListener(gameUI.ChangeAbilityStatus);
        }

    }

    protected bool HasEnoughMana()
    {
        Mana playerMana = this.gameObject.GetComponent<Mana>();
        return playerMana.StatValue >= manaCost ? true : false;
    }

    
}
