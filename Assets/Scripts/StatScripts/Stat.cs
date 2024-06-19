using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StatChangedEvent : UnityEvent<int> {}

public class Stat : MonoBehaviour
{
    protected StatChangedEvent onStatChanged;
    private int statValue;
    public int StatValue
    {
        get { return statValue; }
        set { statValue = value;
              onStatChanged.Invoke(statValue);
        }
   }

    protected virtual void OnStatFinished()
    {
        if(statValue == 0)
        {
            Debug.Log("Stat finished");
        }
    }

    void Awake()
    {
        statValue = 100;
        onStatChanged = new StatChangedEvent();
    }

    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");
        GameUI gameUI = gameController.GetComponent<GameUI>();
        if(gameUI != null)
        {
            onStatChanged.AddListener(gameUI.ChangeManaText);
        }
    }
}
