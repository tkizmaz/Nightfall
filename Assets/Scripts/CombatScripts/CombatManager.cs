using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public static CombatManager instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void DealDamage(Enemy enemy, int damage)
    {
        enemy.Health.StatValue -= damage;
    }
}
