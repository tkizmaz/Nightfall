using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Stat
{
    protected override void OnStatFinished()
    {
        Debug.Log("Health finished");
    }
}
