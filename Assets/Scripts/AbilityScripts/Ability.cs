using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private int manaCost;
    public int ManaCost
    {
        get { return manaCost; }
        set { manaCost = value; }
    }

    protected virtual void PerformAbility()
    {
        Debug.Log("Ability performed");
    }
}
