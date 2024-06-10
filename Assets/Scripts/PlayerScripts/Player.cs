using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Health health;
    private Mana mana;
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
    // Start is called before the first frame update
    void Start()
    {
        health = this.gameObject.AddComponent<Health>();
        mana = this.gameObject.AddComponent<Mana>();
        Debug.Log(health.StatValue);
    }
}
