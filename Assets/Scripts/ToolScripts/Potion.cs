using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Tool
{
    private int restoreValue;
    public int RestoreValue
    {
        get { return restoreValue; }
        set { restoreValue = value; }
    }

    protected virtual void RestoreStat()
    {
        Debug.Log("Restored " + restoreValue + " points");
    }
}
