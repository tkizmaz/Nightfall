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
    public UnityEvent onStatFinished;
    public StatChangedEvent onStatChanged;
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
              if(statValue <= 0)
              {
                onStatFinished?.Invoke();  
              }
        }
   }

    protected virtual void Awake()
    {
        statValue = maxStatValue;
        onStatChanged = new StatChangedEvent();
        onStatFinished = new UnityEvent();
    }
}
