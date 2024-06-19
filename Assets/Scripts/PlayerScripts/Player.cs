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

    Blink blink;

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

    void Start()
    {
        health = this.gameObject.AddComponent<Health>();
        mana = this.gameObject.AddComponent<Mana>();
        blink = this.gameObject.GetComponent<Blink>();
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
}
