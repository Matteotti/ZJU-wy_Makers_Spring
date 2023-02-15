using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float checkDistance;
    public float checkVertical;
    public float checkHorizontal;
    public float counter;
    public float maxAllowTime;
    public bool isGrounded;
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Collider2D[] groundCheckCollider = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(checkHorizontal, checkVertical, 0), checkDistance);
        //Debug.DrawRay(this.transform.position + new Vector3(checkHorizontal, checkVertical, 0), Vector2.down * checkDistance);
        //Debug.DrawRay(this.transform.position + new Vector3(checkHorizontal, checkVertical, 0), Vector2.up * checkDistance);
        //Debug.DrawRay(this.transform.position + new Vector3(checkHorizontal, checkVertical, 0), Vector2.left * checkDistance);
        //Debug.DrawRay(this.transform.position + new Vector3(checkHorizontal, checkVertical, 0), Vector2.right * checkDistance);
        isGrounded = false;
        foreach (Collider2D collider in groundCheckCollider)
        {
            if(collider.CompareTag("Ground") || collider.CompareTag("Wall") || collider.CompareTag("EnemyBody"))
            {
                isGrounded = true;
            }
            if(!this.GetComponent<PlayerController>().isTakingCover)
            {
                if (this.GetComponent<PlayerController>().isGrounded)
                {
                    animator.SetBool("IsGrounded", true);
                }
                else
                {
                    animator.SetBool("IsGrounded", false);
                }
            }
        }
        if(isGrounded)
        {
            this.GetComponent<PlayerController>().isGrounded = true;
            counter = 0;
        }
        else
        {
            if(counter < maxAllowTime)
            {
                counter += Time.deltaTime;
            }
            else
            {
                counter = 0;
                this.GetComponent<PlayerController>().isGrounded = false;
            }
        }
    }
    //private void Move(Vector2 speed)
    //{
    //    this.GetComponent<Rigidbody2D>().velocity = speed;
    //}
}
