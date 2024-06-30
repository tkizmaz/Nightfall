using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    public void PlayWalkAnimation(bool isWalking)
    {
        playerAnimator.SetBool("isWalking", isWalking);
    }

    public void PlaySlashAnimation(bool isSlashing)
    {
        playerAnimator.SetBool("isSlashing", isSlashing);
    }
}
