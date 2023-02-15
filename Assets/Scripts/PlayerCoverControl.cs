using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoverControl : MonoBehaviour
{
    public float inputHorizontal;
    public float coverJumpSpeed;
    public float coverMoveSpeed;
    public float idleCounter;
    public float idleGap;
    public float checkHorizontal;
    public float checkDistance;
    public int jumpTimes;
    public bool isGrounded;
    public Rigidbody2D rb;
    public Animator animator;
    private void OnEnable()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        idleCounter = 0;
    }
    private void Update()
    {
        CheckAllowQuit();
        if (Input.GetKeyDown(KeyCode.Space) && jumpTimes == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, coverJumpSpeed);
            idleCounter = 0;
            jumpTimes++;
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //attack
            animator.SetBool("IsAttacking", true);
        }
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (!isGrounded)
        {
            rb.velocity = new Vector2(inputHorizontal * coverMoveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (!animator.GetBool("IsIdle") && isGrounded)
        {
            if (idleCounter < idleGap)
            {
                idleCounter += Time.deltaTime;
            }
            else
            {
                idleCounter = 0;
                animator.SetBool("IsIdle", true);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpTimes = 0;
            animator.SetBool("IsJumping", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void CheckAllowQuit()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.parent.position + new Vector3 (checkHorizontal, 0, 0), this.transform.parent.localScale.x * Vector2.left, checkDistance, 1 << 6 | 1 << 7 | 1 << 9 | 1 << 10);
        Debug.DrawRay(this.transform.parent.position + new Vector3(checkHorizontal, 0, 0), this.transform.parent.localScale.x * Vector2.left * checkDistance);
        if(hit.collider != null || !isGrounded)
            this.GetComponentInParent<PlayerTakeCover>().allowQuit = false;
        else
            this.GetComponentInParent<PlayerTakeCover>().allowQuit = true;
    }
}
