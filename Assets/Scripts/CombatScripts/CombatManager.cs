using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public static CombatManager instance;
    [SerializeField]
    CameraEffectController cameraEffectController;

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
        enemy.OnTakeDamage();
    }

    public void DealDamageToPlayer(int damage)
    {
        player.Health.StatValue -= damage;
        cameraEffectController.ApplyOnDamageEffects();
    }
}
