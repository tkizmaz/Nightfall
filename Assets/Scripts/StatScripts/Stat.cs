using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    private int statValue;
    public int StatValue
    {
        get { return statValue; }
        set { statValue = value; }
    }

    protected virtual void OnStatFinished()
    {
        if(statValue == 0)
        {
            Debug.Log("Stat finished");
        }
    }

    void Start()
    {
        statValue = 100;
    }
}
