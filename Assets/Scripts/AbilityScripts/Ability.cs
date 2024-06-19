using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class AbilityUsedEvent : UnityEvent<int> {}
public class Ability : MonoBehaviour
{
    protected float cooldownDuration;
    protected int manaCost;
    protected bool isReadyToPerform = true;
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
    }

    protected virtual void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        isReadyToPerform = true;
    }
}
