using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int jumpTimes;
    public float enemyDefine;
    public float PosX1;
    public float PosX2;
    public float minPosNeeded;
    public float jumpSpeed;
    public float moveSpeed;
    public float jumpCounter;
    public float jumpGap;
    public float groundCheckDis;
    public float minSpeedAllowed;
    public float wallCheckHorizontal;
    //public Animator animator;
    public Rigidbody2D rb;
    public bool facingRight = true;
    public bool isSpottedPlayer;
    public bool isPlugging;
    public bool isGrounded;
    public bool allowLeft;
    public bool allowRight;
    public bool isBroken;
    public GameObject Player;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemyDefine = this.transform.GetChild(1).gameObject.GetComponent<BoxControll>().coverDefineNum;
        Player = GameObject.Find("Player");
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (facingRight)
        {
            this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        if (isPlugging)
        {
            rb.velocity = Vector3.zero;
        }
        else if (isGrounded)
        {
            CheckIfAllowJumping();
            //if (Player.GetComponent<PlayerController>().standardSpeed != moveSpeed / minPosNeeded)
            //{
            //    Player.GetComponent<PlayerController>().standardSpeed = moveSpeed / minPosNeeded;
            //    //Debug.Log("GET!");
            //}
            if (jumpCounter < jumpGap)
            {
                jumpCounter += Time.deltaTime;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                jumpCounter = 0f;
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpTimes++;
            }
        }
        else
        {
            rb.velocity = new Vector2((facingRight ? 1 : -1) * moveSpeed, rb.velocity.y);
        }
    }
    void CheckIfAllowJumping()
    {
        if (jumpTimes > 0)
        {
            RaycastHit2D leftGroundCheck = Physics2D.Raycast(this.transform.position - new Vector3(minPosNeeded, 0, 0) + new Vector3 (0, wallCheckHorizontal, 0), Vector2.down, groundCheckDis, 1 << 7);
            Debug.DrawRay(this.transform.position - new Vector3(minPosNeeded, 0, 0) + new Vector3(0, wallCheckHorizontal, 0), Vector2.down * groundCheckDis);
            RaycastHit2D rightGroundCheck = Physics2D.Raycast(this.transform.position + new Vector3(minPosNeeded, 0, 0) + new Vector3(0, wallCheckHorizontal, 0), Vector2.down, groundCheckDis, 1 << 7);
            Debug.DrawRay(this.transform.position + new Vector3(minPosNeeded, 0, 0) + new Vector3(0, wallCheckHorizontal, 0), Vector2.down * groundCheckDis);
            RaycastHit2D leftWallCheck = Physics2D.Raycast(this.transform.position + new Vector3(0, wallCheckHorizontal, 0), Vector2.left, minPosNeeded, (1 << 3 | 1 << 6 | 1 << 10));
            Debug.DrawRay(this.transform.position + new Vector3(0, wallCheckHorizontal, 0), Vector2.left * minPosNeeded);
            RaycastHit2D rightWallCheck = Physics2D.Raycast(this.transform.position + new Vector3(0, wallCheckHorizontal, 0), Vector2.right, minPosNeeded, (1 << 3 | 1 << 6 | 1 << 10));
            Debug.DrawRay(this.transform.position + new Vector3(0, wallCheckHorizontal, 0), Vector2.right * minPosNeeded);
            allowLeft = true;
            allowRight = true;
            //Debug.Log(leftGroundCheck.collider);
            //Debug.Log(rightGroundCheck.collider);
            //Debug.Log(leftWallCheck.collider);
            //Debug.Log(rightWallCheck.collider);
            if (leftGroundCheck.collider == null)
            {
                allowLeft = false;
            }
            if (rightGroundCheck.collider == null)
            {
                allowRight = false;
            }
            if (leftWallCheck.collider != null)
            {
                allowLeft = false;
            }
            if (rightWallCheck.collider != null)
            {
                allowRight = false;
            }
            //用这个变量来改变方向，两边不允许就自毁
            if (!allowLeft && !allowRight)
            {
                isBroken = true;
                //播放动画，然后自毁
                this.GetComponent<EnemyDestory>().DestroyWithBody();
            }
            else if (facingRight && !allowRight)
            {
                facingRight = false;
            }
            else if (!facingRight && !allowLeft)
            {
                facingRight = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (jumpTimes == 0)
            {
                PosX1 = transform.position.x;
            }
            if (jumpTimes == 1)
            {
                PosX2 = transform.position.x;
                minPosNeeded = Mathf.Abs(PosX2 - PosX1);
            }
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("GoodEnemy"))
        {
            facingRight = !facingRight;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if(facingRight && collision.gameObject.transform.position.x > this.transform.position.x)
            {
                facingRight = !facingRight;
            }
            else if(!facingRight && collision.gameObject.transform.position.x < this.transform.position.x)
            {
                facingRight = !facingRight;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

