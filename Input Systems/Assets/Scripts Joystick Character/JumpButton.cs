using UnityEngine;

public class JumpButton : MonoBehaviour
{
    public Animator playerAnimator;
    public void OnJump()
    {
        playerAnimator.SetTrigger("isJumping");
    }
}