using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class AbilityUsedEvent : UnityEvent<int> {}

[System.Serializable]
public class AbilityStatusEvent : UnityEvent<AbilityType, bool, bool> {}

[System.Serializable]
public class AbilitySelectionEvent : UnityEvent<AbilityType, bool> {}

public enum AbilityType
{
    Blink,
    BendTime,
    DarkVision
}

public class Ability : MonoBehaviour
{
    protected bool isAbilitySelected = false;
    protected AbilityType abilityType;
    protected float cooldownDuration;
    protected int manaCost;
    protected bool isReadyToPerform = true;
    public AbilityUsedEvent abilityUsed;
    public AbilityStatusEvent isAbilityReady;
    public AbilitySelectionEvent isAbilitySelectedEvent;
    PlayerSoundManager playerSoundManager;
    
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

    public bool IsAbilitySelected
    {
        get { return isAbilitySelected; }
        set { isAbilitySelected = value; 
              isAbilitySelectedEvent.Invoke(abilityType, isAbilitySelected);
        }
    }

    protected virtual void PerformAbility()
    {
        playerSoundManager.PlayAbilitySfx(abilityType);
        isReadyToPerform = false;
        isAbilityReady.Invoke(abilityType, isReadyToPerform, isAbilitySelected);
        abilityUsed.Invoke(manaCost);
        StartCooldown();
    }

    protected virtual void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        isReadyToPerform = true;
        isAbilityReady.Invoke(abilityType, isReadyToPerform, isAbilitySelected);
    }

    private void Start() 
    {
        playerSoundManager = this.gameObject.GetComponent<PlayerSoundManager>();
        Mana playerMana = this.gameObject.GetComponent<Mana>();
        if(playerMana != null)
        {
            abilityUsed.AddListener(playerMana.OnAbilityUsed);
        }
        GameUI gameUI = GameObject.FindWithTag("GameController").GetComponent<GameUI>();
        if(gameUI != null)
        {
            isAbilityReady.AddListener(gameUI.ChangeAbilityStatus);
            isAbilitySelectedEvent.AddListener(gameUI.ChangeAbilitySelection);
        }
    }

    protected bool HasEnoughMana()
    {
        Mana playerMana = this.gameObject.GetComponent<Mana>();
        return playerMana.StatValue >= manaCost ? true : false;
    }

    public void SelectAbility()
    {
        isAbilitySelected = true;
    }
    
}
