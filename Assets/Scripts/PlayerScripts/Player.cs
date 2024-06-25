using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Health health;
    private Mana mana;
    private List<HealthPotion> healthPotions = new List<HealthPotion>();
    private List<ManaPotion> manaPotions = new List<ManaPotion>();
    [SerializeField]
    private KeyCode healthPotionKey = KeyCode.R;
    [SerializeField]
    private KeyCode manaPotionKey = KeyCode.T;
    private Ability selectedAbility;
    private Blink blink;
    private BendTime bendTime;

    public Health Health
    {
        get { return health; }
        set { health = value; }
    }

    public Mana Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    void Awake()
    {
        health = this.gameObject.AddComponent<Health>();
        mana = this.gameObject.AddComponent<Mana>();
        blink = this.gameObject.GetComponent<Blink>();
        bendTime = this.gameObject.GetComponent<BendTime>();
        AssignInitialPotions();
    }

    private void AssignInitialPotions()
    {
        for(int i = 0; i < GameManager.instance.initialPotionCount; i++)
        {
            HealthPotion healthPotion = this.gameObject.AddComponent<HealthPotion>();
            healthPotions.Add(healthPotion);
            ManaPotion manaPotion = this.gameObject.AddComponent<ManaPotion>();
            manaPotions.Add(manaPotion);
        }
    }

    private void CheckPotionUse()
    {
        if(Input.GetKeyDown(healthPotionKey) && healthPotions.Count > 0 && health.StatValue < health.MaxStatValue)
        {
            HealthPotion healthPotion = healthPotions[0];
            healthPotions.RemoveAt(0);
            healthPotion.RestoreStat();
            Destroy(healthPotion);
        }
        if(Input.GetKeyDown(manaPotionKey) && manaPotions.Count > 0 && mana.StatValue < mana.MaxStatValue)
        {
            ManaPotion manaPotion = manaPotions[0];
            manaPotions.RemoveAt(0);
            manaPotion.RestoreStat();
            Destroy(manaPotion);
        }
    }

    private void Update() 
    {
        CheckPotionUse();    
        CheckAbilitySelection();
    }

    public int GetPotionCount(StatType statType)
    {
        return statType == StatType.Health ? healthPotions.Count : manaPotions.Count;
    }

    private void CheckAbilitySelection()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedAbility = blink;
            blink.IsAbilitySelected = true;
            bendTime.IsAbilitySelected = false;
        }

        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedAbility = bendTime;
            bendTime.IsAbilitySelected = true;
            blink.IsAbilitySelected = false;
        }
    }
}
