using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void ExitHurt()
    {
        animator.SetBool("IsHurt", false);
        this.GetComponent<PlayerController>().isHurt = false;
        this.GetComponent<PlayerController>().HP--;
    }
    void ExitJump()
    {
        animator.SetBool("IsAiring", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("StartFall", false);
        animator.SetBool("Falling", false);
        animator.SetBool("FallToGround", false);
        animator.SetBool("StartJump", false);
    }
}
