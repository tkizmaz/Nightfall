using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthState
{
    Alive,
    Dead,
    Critical
}

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
    private DarkVision darkVision;
    private Dictionary<KeyCode, Ability> abilityDict = new Dictionary<KeyCode, Ability>();
    private HealthState healthState;
    public HealthState HealthState => healthState;
    private PlayerAnimationController playerAnimationController;
    [SerializeField]
    private Weapon sword;
    [SerializeField]
    private Weapon pistol;
    private Weapon selectedWeapon;

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
        AssignProperties();
        AssignInitialPotions();
        AssignAbilitiesToKeys();
    }

    private void Start() 
    {
        selectedWeapon = sword;
        playerAnimationController = this.gameObject.GetComponent<PlayerAnimationController>();
        healthState = HealthState.Alive;
        GameObject gameController = GameObject.FindWithTag("GameController");
        GameUI gameUI = gameController.GetComponent<GameUI>();
        if(gameUI != null)
        {
            health.onStatChanged.AddListener(gameUI.ChangeStatText);
            mana.onStatChanged.AddListener(gameUI.ChangeStatText);
            health.onStatFinished.AddListener(this.OnDeath);
            health.onStatFinished.AddListener(playerAnimationController.PlayDeathAnimation);
        }
    }

    private void AssignProperties()
    {
        health = this.gameObject.AddComponent<Health>();
        mana = this.gameObject.AddComponent<Mana>();
        blink = this.gameObject.GetComponent<Blink>();
        bendTime = this.gameObject.GetComponent<BendTime>();
        darkVision = this.gameObject.GetComponent<DarkVision>();
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

    private void AssignAbilitiesToKeys()
    {
        abilityDict.Add(KeyCode.Alpha1, blink);
        abilityDict.Add(KeyCode.Alpha2, bendTime);
        abilityDict.Add(KeyCode.Alpha3, darkVision);
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
        CheckWeaponChange();
        CheckAttack();
    }

    public int GetPotionCount(StatType statType)
    {
        return statType == StatType.Health ? healthPotions.Count : manaPotions.Count;
    }

    private void CheckAbilitySelection()
    {
        foreach(KeyCode key in abilityDict.Keys)
        {
            if(Input.GetKeyDown(key))
            {
                selectedAbility = abilityDict[key];
                selectedAbility.IsAbilitySelected = true;
                foreach(Ability ability in abilityDict.Values)
                {
                    if(ability != selectedAbility)
                    {
                        ability.IsAbilitySelected = false;
                    }
                }
            }
        }
    }

    private void OnDeath()
    {
        healthState = HealthState.Dead;
    }

    private void CheckWeaponChange()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(selectedWeapon == sword)
            {
                sword.DeactivateWeapon();
                selectedWeapon = pistol;
            }
            else
            {
                pistol.DeactivateWeapon();
                selectedWeapon = sword;
            }
            selectedWeapon.ActivateWeapon();
        }
    }

    private void CheckAttack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            selectedWeapon.PerformAttack();
        }
    }
}
