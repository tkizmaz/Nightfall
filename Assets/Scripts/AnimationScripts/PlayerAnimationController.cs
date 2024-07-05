using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    string SLASH_ANIMATION_1 = "Slash";
    string SLASH_ANIMATION_2 = "BackSlash";

    [SerializeField]
    private Animator playerAnimator;

    public void PlayWalkAnimation(bool isWalking)
    {
        playerAnimator.SetBool("isWalking", isWalking);
    }

    public void PlaySlashAnimation()
    {
        int randomSlash = Random.Range(0, 2);
        string slashAnimation = randomSlash == 0 ? SLASH_ANIMATION_1 : SLASH_ANIMATION_2;
        playerAnimator.SetTrigger(slashAnimation);
    }
}
