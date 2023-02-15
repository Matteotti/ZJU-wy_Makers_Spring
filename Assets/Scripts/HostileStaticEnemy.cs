using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileStaticEnemy : MonoBehaviour
{
    public int jumpTimes;
    public int enemyDefine;
    public int playerAllowed;
    public int attackJumpTime;
    public float playerCheckVertical;
    public float playerCheckDistance;
    public float playerCheckCounter;
    public float playerCheckLastTime;
    public float playerCheckChangeDirectionCounter;
    public float playerCheckChangeDirectionTime;
    public float backPlayerCheckDistance;
    public float backPlayerCheckHorizontal;
    public float PosX1;
    public float PosX2;
    public float minPosNeeded;
    public float playerPosX;
    public float enemyPosX;
    public float jumpSpeed;
    public float moveSpeed;
    public float chaseSpeed;
    public float jumpCounter;
    public float jumpGap;
    public float groundCheckDis;
    public float minSpeedAllowed;
    public float playerJumpCounter;
    public float playerJumpMaxTimeAllowed;
    public float playerMoveCounter;
    public float playerMoveMaxTimeAllowed;
    public float attackDeltaDistance;
    public float attackDistance;
    public Animator animator;
    public Rigidbody2D rb;
    public bool facingRight = true;
    public bool isSpottedPlayer;
    public bool isPlugging;
    public bool isGrounded;
    public bool allowLeft;
    public bool allowRight;
    public bool isBroken;
    public bool allowBlack;
    public bool allowWhite;
    public bool allowGolden;
    public bool attack;
    public bool attackEnemy;
    public GameObject Player;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemyDefine = this.transform.GetChild(1).gameObject.GetComponent<BoxControll>().coverDefineNum;
        Player = GameObject.Find("Player");
        Invoke("CheckWhichAllow", 0.2f);
        animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        JudgeIfSpottedPlayer();
        CheckIfAttack();
        Move();
        Freeze();
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
        if (attackEnemy)
        {
            allowLeft = true;
            allowRight = true;
            jumpCounter = 0;
            if (isGrounded && attackJumpTime < 2)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                animator.SetBool("IsJumping", false);
                chaseSpeed = Player.GetComponent<PlayerController>().standardSpeed * (Mathf.Abs(enemyPosX - this.transform.position.x) - attackDeltaDistance);
                if (chaseSpeed < 0)
                    chaseSpeed = 0;
            }
            else if (attackJumpTime < 2)
            {
                if (this.transform.position.x < enemyPosX)
                {
                    facingRight = true;
                }
                else
                {
                    facingRight = false;
                }
                animator.SetBool("IsJumping", true);
                rb.velocity = new Vector2((facingRight ? 1 : -1) * chaseSpeed, rb.velocity.y);
            }
            else if (isGrounded)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsAttack", true);
            }
        }
        else if (attack)
        {
            allowLeft = true;
            allowRight = true;
            jumpCounter = 0;
            if (isGrounded && attackJumpTime < 2)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                animator.SetBool("IsJumping", false);
                chaseSpeed = Player.GetComponent<PlayerController>().standardSpeed * (Mathf.Abs(playerPosX - this.transform.position.x) - attackDeltaDistance);
                if (chaseSpeed < 0)
                    chaseSpeed = 0;
            }
            else if (attackJumpTime < 2)
            {
                if (this.transform.position.x < playerPosX)
                {
                    facingRight = true;
                }
                else
                {
                    facingRight = false;
                }
                animator.SetBool("IsJumping", true);
                rb.velocity = new Vector2((facingRight ? 1 : -1) * chaseSpeed, rb.velocity.y);
            }
            else if (isGrounded)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsAttack", true);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else if (isPlugging)
        {
            rb.velocity = Vector3.zero;
        }
    }
    void JudgeIfSpottedPlayer()
    {
        RaycastHit2D playerCheck = Physics2D.Raycast(transform.position + new Vector3(0, playerCheckVertical, 0), facingRight ? Vector2.right : Vector2.left, playerCheckDistance, ~(1 << this.gameObject.layer));
        Debug.DrawRay(transform.position + new Vector3(0, playerCheckVertical, 0), facingRight ? Vector2.right * playerCheckDistance : Vector2.left * playerCheckDistance);
        if (playerCheck.collider != null && playerCheck.collider.CompareTag("Player"))
        {
            isSpottedPlayer = true;
            playerCheckCounter = 0;
            playerPosX = playerCheck.collider.transform.position.x - 0.5f;
        }
        else
        {
            playerCheckCounter += Time.deltaTime;
            if (playerCheckCounter >= playerCheckLastTime)
            {
                isSpottedPlayer = false;
                playerCheckCounter = 0;
            }
        }
        if (isSpottedPlayer)
        {
            RaycastHit2D playerCheckBack = Physics2D.Raycast(this.transform.position + new Vector3(0, playerCheckVertical, 0), facingRight ? Vector2.left : Vector2.right, backPlayerCheckDistance, ~(1 << this.gameObject.layer | 1 << 11));
            //Debug.Log(playerCheckBack.collider);
            Debug.DrawRay(this.transform.position + new Vector3(0, playerCheckVertical, 0), backPlayerCheckDistance * (facingRight ? Vector2.left : Vector2.right));
            if (playerCheckBack.collider != null && playerCheckBack.collider.CompareTag("Player"))
            {
                isSpottedPlayer = true;
                playerCheckCounter = 0;
                playerPosX = playerCheckBack.collider.transform.position.x - 0.5f;
            }
            else
            {
                playerCheckCounter += Time.deltaTime;
                if (playerCheckCounter >= playerCheckLastTime)
                {
                    isSpottedPlayer = false;
                    //CheckIfAllowJumping();
                    playerCheckCounter = 0;
                }
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
            if (attack || attackEnemy)
            {
                attackJumpTime++;
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
            if (attack || attackEnemy)
            {
                attackJumpTime++;
            }
        }
    }
    void CheckWhichAllow()
    {
        RaycastHit2D left = Physics2D.Raycast(this.transform.position, Vector2.left, float.PositiveInfinity, 1 << 3);
        RaycastHit2D right = Physics2D.Raycast(this.transform.position, Vector2.right, float.PositiveInfinity, 1 << 3);
        RaycastHit2D[] leftEnemy = Physics2D.RaycastAll(this.transform.position, Vector2.left, this.transform.position.x - left.point.x, 1 << 9);
        RaycastHit2D[] rightEnemy = Physics2D.RaycastAll(this.transform.position, Vector2.right, right.point.x - this.transform.position.x, 1 << 9);
        //Debug.DrawRay(this.transform.position, Vector2.left * (this.transform.position.x - left.point.x));
        //Debug.DrawRay(this.transform.position, Vector2.right * (right.point.x - this.transform.position.x));
        foreach (var enemy in leftEnemy)
        {
            //Debug.Log(enemy + this.gameObject.name);
            if (enemy.collider.GetComponent<HostileEnemyMove>() != null && enemy.collider.GetComponent<HostileEnemyMove>().enemyDefine == 1)
            {
                allowBlack = false;
            }
            else if (enemy.collider.GetComponent<HostileStaticEnemy>() != null && enemy.collider.GetComponent<HostileStaticEnemy>().enemyDefine == 1)
            {
                allowBlack = false;
            }
            if (enemy.collider.GetComponent<HostileEnemyMove>() != null && enemy.collider.GetComponent<HostileEnemyMove>().enemyDefine == 2)
            {
                allowWhite = false;
            }
            else if (enemy.collider.GetComponent<HostileStaticEnemy>() != null && enemy.collider.GetComponent<HostileStaticEnemy>().enemyDefine == 2)
            {
                allowWhite = false;
            }
            if (enemy.collider.GetComponent<HostileEnemyMove>() != null && enemy.collider.GetComponent<HostileEnemyMove>().enemyDefine == 3)
            {
                allowGolden = false;
            }
            else if (enemy.collider.GetComponent<HostileStaticEnemy>() != null && enemy.collider.GetComponent<HostileStaticEnemy>().enemyDefine == 3)
            {
                allowGolden = false;
            }
        }
        foreach (var enemy in rightEnemy)
        {
            //Debug.Log(enemy + this.gameObject.name);
            if (enemy.collider.GetComponent<HostileEnemyMove>() != null && enemy.collider.GetComponent<HostileEnemyMove>().enemyDefine == 1)
            {
                allowBlack = false;
            }
            else if (enemy.collider.GetComponent<HostileStaticEnemy>() != null && enemy.collider.GetComponent<HostileStaticEnemy>().enemyDefine == 1)
            {
                allowBlack = false;
            }
            if (enemy.collider.GetComponent<HostileEnemyMove>() != null && enemy.collider.GetComponent<HostileEnemyMove>().enemyDefine == 2)
            {
                allowWhite = false;
            }
            else if (enemy.collider.GetComponent<HostileStaticEnemy>() != null && enemy.collider.GetComponent<HostileStaticEnemy>().enemyDefine == 2)
            {
                allowWhite = false;
            }
            if (enemy.collider.GetComponent<HostileEnemyMove>() != null && enemy.collider.GetComponent<HostileEnemyMove>().enemyDefine == 3)
            {
                allowGolden = false;
            }
            else if (enemy.collider.GetComponent<HostileStaticEnemy>() != null && enemy.collider.GetComponent<HostileStaticEnemy>().enemyDefine == 3)
            {
                allowGolden = false;
            }
        }
    }
    void CheckIfAttack()
    {
        if (isSpottedPlayer)
        {
            if (Player.GetComponent<PlayerTakeCover>().coverBeTakenDefine == 0)
            {
                attack = true;
            }
            else if (Player.GetComponent<PlayerTakeCover>().coverBeTakenDefine == 1 && !allowBlack)
            {
                attack = true;
            }
            else if (Player.GetComponent<PlayerTakeCover>().coverBeTakenDefine == 2 && !allowWhite)
            {
                attack = true;
            }
            else if (Player.GetComponent<PlayerTakeCover>().coverBeTakenDefine == 3 && !allowGolden)
            {
                attack = true;
            }
            else
            {
                //¸øÎÒÌø£¡
                if (Player.transform.GetChild(2).GetComponent<PlayerCoverControl>().isGrounded)
                {
                    if (playerJumpCounter < playerJumpMaxTimeAllowed)
                    {
                        playerJumpCounter += Time.deltaTime;
                    }
                    else
                    {
                        attack = true;
                        playerJumpCounter = 0;
                    }
                }
                else
                {
                    playerJumpCounter = 0;
                }
            }
        }
        else
        {
            attack = false;
            playerJumpCounter = 0;
            playerMoveCounter = 0;
            //Player.GetComponent<PlayerCoverBroke>().isBroke = false;
        }
    }
    void Freeze()
    {
        if (attack || attackEnemy)
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
    }
    void StopAttack()
    {
        animator.SetBool("IsAttack", false);
        attackJumpTime = 0;
    }
    void TryHitPlayer()
    {
        bool hitPlayer = false;
        GameObject player = null;
        RaycastHit2D tryHitPlayerCheck = Physics2D.Raycast(transform.position + new Vector3(0, playerCheckVertical, 0), facingRight ? Vector2.right : Vector2.left, attackDistance, ~(1 << this.gameObject.layer));
        if (tryHitPlayerCheck.collider != null && tryHitPlayerCheck.collider.CompareTag("Player"))
        {
            hitPlayer = true;
            player = tryHitPlayerCheck.collider.gameObject;
        }
        tryHitPlayerCheck = Physics2D.Raycast(transform.position + new Vector3(0, 0, 0), facingRight ? Vector2.right : Vector2.left, attackDistance, ~(1 << this.gameObject.layer));
        if (tryHitPlayerCheck.collider != null && tryHitPlayerCheck.collider.CompareTag("Player"))
        {
            hitPlayer = true;
            player = tryHitPlayerCheck.collider.gameObject;
        }
        tryHitPlayerCheck = Physics2D.Raycast(transform.position + new Vector3(0, -playerCheckVertical, 0), facingRight ? Vector2.right : Vector2.left, attackDistance, ~(1 << this.gameObject.layer));
        if (tryHitPlayerCheck.collider != null && tryHitPlayerCheck.collider.CompareTag("Player"))
        {
            hitPlayer = true;
            player = tryHitPlayerCheck.collider.gameObject;
        }
        if (hitPlayer && player.GetComponent<PlayerHurtHarmless>() != null && player.GetComponent<PlayerHurtHarmless>().allowHurt)
        {
            player.GetComponent<PlayerController>().isHurt = true;
            player.GetComponent<PlayerHurtHarmless>().allowHurt = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2((facingRight ? 1 : -1) * player.GetComponent<PlayerEnemyInterface>().bounceSpeed, 0);
            player.GetComponent<PlayerEnemyInterface>().timerBegin = true;
        }
        else if (hitPlayer && player.GetComponentInParent<PlayerHurtHarmless>() != null && player.GetComponentInParent<PlayerHurtHarmless>().allowHurt)
        {
            player.GetComponentInParent<Animator>().SetBool("IsBroken", true);
            player.GetComponentInParent<PlayerController>().jumpTime = 1;
            player.GetComponentInParent<PlayerController>().isJumpButtonDown = false;
            Destroy(player.GetComponentInParent<PlayerTakeCover>().nowSpring);
            player.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
