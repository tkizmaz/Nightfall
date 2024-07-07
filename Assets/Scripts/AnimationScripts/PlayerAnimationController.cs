using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    string SLASH_ANIMATION_1 = "Slash";
    string SLASH_ANIMATION_2 = "BackSlash";
    private Animator playerAnimator;
    [HideInInspector]
    public bool isAttackFinished = false;

    private void Awake() 
    {
        playerAnimator = GetComponent<Animator>();
    }
    public void PlayWalkAnimation(bool isWalking)
    {
        playerAnimator.SetBool("isWalking", isWalking);
    }

    public void PlaySprintAnimation(bool isSprinting)
    {
        playerAnimator.SetBool("isSprinting", isSprinting);
    }

    public void PlaySlashAnimation()
    {
        isAttackFinished = false;
        int randomSlash = Random.Range(0, 2);
        string slashAnimation = randomSlash == 0 ? SLASH_ANIMATION_1 : SLASH_ANIMATION_2;
        playerAnimator.SetTrigger(slashAnimation);
    }

    public void PlayKickFinisher()
    {
        playerAnimator.SetTrigger("KickFinisher");
    }

    public void SetAttackFinished()
    {
        isAttackFinished = true;
    }

    public void KickSfxRoutine()
    {
        AudioManager.instance.PlayKickSfx();
    }

    public void SwordSwingSfxRoutine()
    {
        AudioManager.instance.PlaySwordSwingSfx();
    }

    public void SwordHitEnemySfxRoutine()
    {
        AudioManager.instance.PlaySwordHitEnemySfx();
    }

}
