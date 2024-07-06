using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherAnimationController : MonoBehaviour
{
    public static FinisherAnimationController instance;

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

    public void PlayFinisherAnimation(PlayerAnimationController playerAnimationController, Enemy enemy)
    {
        playerAnimationController.PlayKickFinisher();
        enemy.PlayKickFinisherEnemy();
    }
}
