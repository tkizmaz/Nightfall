using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    string SLASH_ANIMATION_1 = "Slash";

    [SerializeField]
    private Animator playerAnimator;

    public void PlayWalkAnimation(bool isWalking)
    {
        playerAnimator.SetBool("isWalking", isWalking);
    }

    public void PlaySlashAnimation()
    {
        playerAnimator.SetTrigger(SLASH_ANIMATION_1);
    }
}
