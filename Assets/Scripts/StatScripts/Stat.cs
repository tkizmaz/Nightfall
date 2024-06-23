using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum StatType
{
    Health,
    Mana
}

[System.Serializable]
public class StatChangedEvent : UnityEvent<StatType, int> {}

public class Stat : MonoBehaviour
{
    protected StatChangedEvent onStatChanged;
    protected StatType statType;
    private int statValue;
    private int maxStatValue = 100;

    public int MaxStatValue
    {
        get { return maxStatValue; }
        set { maxStatValue = value; }
    }
    public int StatValue
    {
        get { return statValue; }
        set { statValue = value;
              onStatChanged.Invoke(statType, statValue);
        }
   }

    protected virtual void OnStatFinished()
    {
        if(statValue == 0)
        {
            Debug.Log("Stat finished");
        }
    }

    protected virtual void Awake()
    {
        statValue = maxStatValue;
        onStatChanged = new StatChangedEvent();
    }

    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");
        GameUI gameUI = gameController.GetComponent<GameUI>();
        if(gameUI != null)
        {
            onStatChanged.AddListener(gameUI.ChangeStatText);
        }
    }
}
