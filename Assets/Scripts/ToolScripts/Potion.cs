using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PotionUsedEvent : UnityEvent<StatType, int> {}

public class Potion : Tool
{
    public PotionUsedEvent OnPotionUse;
    protected StatType statType;
    protected int restoreValue;
    public int RestoreValue
    {
        get { return restoreValue; }
        set { restoreValue = value; }
    }

    public virtual void RestoreStat()
    {
        Player player = this.gameObject.GetComponent<Player>();
        Stat playerStat = statType == StatType.Health ? player.Health : player.Mana;
        int valueToRestore = playerStat.StatValue + restoreValue;
        int restoredValue = valueToRestore > playerStat.MaxStatValue ? playerStat.MaxStatValue : valueToRestore;
        playerStat.StatValue = restoredValue;
        OnPotionUse.Invoke(statType, player.GetPotionCount(statType));
    }

    protected virtual void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");
        GameUI gameUI = gameController.GetComponent<GameUI>();
        AudioManager audioManager = gameController.GetComponent<AudioManager>();
        if(gameUI != null)
        {
            OnPotionUse.AddListener(gameUI.ChangePotionCount);
            OnPotionUse.AddListener(audioManager.PlaySwallowSfx);
        }
    }

    private void Awake() 
    {
        OnPotionUse = new PotionUsedEvent();
    }
}
