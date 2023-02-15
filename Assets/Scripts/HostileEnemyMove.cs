using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileEnemyMove : MonoBehaviour
{
    public int jumpTimes;
    public int enemyDefine;
    public int playerAllowed;
    public int attackJumpTime;
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
    public float wallCheckHorizontal;
    public float wallCheckVertical;
    public float playerCheckVertical;
    public float attackDistance;
    public float attackDeltaDistance;
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
    public bool startAttack;
    public bool attackEnemy;
    public bool isBossEnemy;
    public bool isAllowShutDown;
    public GameObject Player;
    public GameObject goldenBody;
    public GameObject whiteBody;
    public GameObject blackBody;
    public GameObject createBox1;
    public GameObject createBox2;
    public GameObject boss;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemyDefine = this.transform.GetChild(1).gameObject.GetComponent<BoxControll>().coverDefineNum;
        Player = GameObject.Find("Player");
        boss = GameObject.Find("King");
        Invoke("CheckWhichAllow", 0.2f);
        animator = this.GetComponent<Animator>();
        if (isBossEnemy)
        {
            backPlayerCheckDistance = 40;
            playerCheckDistance = 40;
            playerJumpMaxTimeAllowed = 0.01f;
        }
    }
    void Update()
    {
        JudgeIfSpottedPlayer();
        CheckIfAttack();
        if (isAllowShutDown)
            Move();
        else
            animator.SetBool("IsJumping", false);
        //Debug.DrawRay(transform.position + new Vector3(0, 0, 0), facingRight ? Vector2.right : Vector2.left * attackDistance);
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
        if(attackEnemy)
        {
            allowLeft = true;
            allowRight = true;
            jumpCounter = 0;
            if (isGrounded && attackJumpTime < 2)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                animator.SetBool("IsJumping", false);
                chaseSpeed = moveSpeed / minPosNeeded * (Mathf.Abs(playerPosX - this.transform.position.x) - attackDeltaDistance);
                if(Mathf.Abs(chaseSpeed) > 500)
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
                chaseSpeed = moveSpeed / minPosNeeded * (Mathf.Abs(playerPosX - this.transform.position.x) - attackDeltaDistance);
                if (Mathf.Abs(chaseSpeed) > 500)
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
            else if(isGrounded)
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
        else if (isGrounded)
        {
            CheckIfAllowJumping();
            if (Player.GetComponent<PlayerController>().standardSpeed != moveSpeed / minPosNeeded && !isBossEnemy)
            {
                Player.GetComponent<PlayerController>().standardSpeed = moveSpeed / minPosNeeded;
                //Debug.Log("GET!");
            }
            if (jumpCounter < jumpGap)
            {
                jumpCounter += Time.deltaTime;
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetBool("IsJumping", false);
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
            animator.SetBool("IsJumping", true);
        }
    }
    void JudgeIfSpottedPlayer()
    {
        RaycastHit2D playerCheck = Physics2D.Raycast(transform.position + new Vector3(0, playerCheckVertical, 0), facingRight ? Vector2.right : Vector2.left, playerCheckDistance, ~(1 << this.gameObject.layer | 1 << 2));
        Debug.DrawRay(transform.position + new Vector3(0, playerCheckVertical, 0), facingRight ? Vector2.right * playerCheckDistance : Vector2.left * playerCheckDistance);
        //Debug.Log("FORWARD" + playerCheck.collider);
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
        if (isSpottedPlayer || isBossEnemy)
        {
            RaycastHit2D playerCheckBack = Physics2D.Raycast(this.transform.position + new Vector3(0, playerCheckVertical, 0), facingRight ? Vector2.left : Vector2.right, backPlayerCheckDistance, ~(1 << this.gameObject.layer | 1 << 11 | 1 << 2));
            //Debug.Log(playerCheckBack.collider);
            Debug.DrawRay(this.transform.position + new Vector3(0, playerCheckVertical, 0), backPlayerCheckDistance * (facingRight ? Vector2.left : Vector2.right));
            //Debug.Log("BACK" + playerCheckBack.collider);
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
    void CheckIfAllowJumping()
    {
        if (jumpTimes > 0)
        {
            RaycastHit2D leftGroundCheck = Physics2D.Raycast(this.transform.position - new Vector3(minPosNeeded, 0, 0) + new Vector3(0, wallCheckVertical, 0), Vector2.down, groundCheckDis, 1 << 7);
            Debug.DrawRay(this.transform.position - new Vector3(minPosNeeded, 0, 0) + new Vector3(0, wallCheckVertical, 0), Vector2.down * groundCheckDis);
            RaycastHit2D rightGroundCheck = Physics2D.Raycast(this.transform.position + new Vector3(minPosNeeded, 0, 0) + new Vector3(0, wallCheckVertical, 0), Vector2.down, groundCheckDis, 1 << 7);
            Debug.DrawRay(this.transform.position + new Vector3(minPosNeeded, 0, 0) + new Vector3(0, wallCheckVertical, 0), Vector2.down * groundCheckDis);
            RaycastHit2D leftWallCheck = Physics2D.Raycast(this.transform.position + new Vector3(-wallCheckHorizontal, wallCheckVertical, 0), Vector2.left, minPosNeeded - wallCheckHorizontal, (1 << 3 | 1 << 6 | 1 << 10 | 1 << 15));
            Debug.DrawRay(this.transform.position + new Vector3(-wallCheckHorizontal, wallCheckVertical, 0), Vector2.left * (minPosNeeded - wallCheckHorizontal));
            RaycastHit2D rightWallCheck = Physics2D.Raycast(this.transform.position + new Vector3(wallCheckHorizontal, wallCheckVertical, 0), Vector2.right, minPosNeeded - wallCheckHorizontal, (1 << 3 | 1 << 6 | 1 << 10 | 1 << 15));
            Debug.DrawRay(this.transform.position + new Vector3(wallCheckHorizontal, wallCheckVertical, 0), Vector2.right * (minPosNeeded - wallCheckHorizontal));
            allowLeft = true;
            allowRight = true;
            //Debug.Log(leftGroundCheck.collider);
            //Debug.Log(rightGroundCheck.collider);
            //Debug.Log("L " + leftWallCheck.collider);
            //Debug.Log("R " + rightWallCheck.collider);
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
            if (!allowLeft && !allowRight && isAllowShutDown)
            {
                isBroken = true;
                if (this.GetComponent<MoveEnemyWarningUI>().thisWarningBar != null)
                {
                    Destroy(this.GetComponent<MoveEnemyWarningUI>().thisWarningBar);
                }
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                animator.SetBool("IsShutDown", true);
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else if (facingRight && !allowRight && !isBossEnemy)
            {
                facingRight = false;
            }
            else if (!facingRight && !allowLeft && !isBossEnemy)
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
            if (attack || attackEnemy)
            {
                attackJumpTime++;
            }
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("GoodEnemy"))
        {
            if(!isBossEnemy)
                facingRight = !facingRight;
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
                //给我跳！
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
    void StopAttack()
    {
        animator.SetBool("IsAttack", false);
        attackJumpTime = 0;
    }
    void TryHitPlayer()
    {
        bool hitPlayer = false;
        GameObject player = null;
        RaycastHit2D tryHitPlayerCheck = Physics2D.Raycast(transform.position + new Vector3(0, playerCheckVertical, 0), facingRight ? Vector2.right : Vector2.left, attackDistance, ~(1 << this.gameObject.layer | 1 << 2));
        if (tryHitPlayerCheck.collider != null && tryHitPlayerCheck.collider.CompareTag("Player"))
        {
            hitPlayer = true;
            player = tryHitPlayerCheck.collider.gameObject;
        }
        tryHitPlayerCheck = Physics2D.Raycast(transform.position + new Vector3(0, 0, 0), facingRight ? Vector2.right : Vector2.left, attackDistance, ~(1 << this.gameObject.layer) | 1 << 2);
        if (tryHitPlayerCheck.collider != null && tryHitPlayerCheck.collider.CompareTag("Player"))
        {
            hitPlayer = true;
            player = tryHitPlayerCheck.collider.gameObject;
        }
        tryHitPlayerCheck = Physics2D.Raycast(transform.position + new Vector3(0, -playerCheckVertical, 0), facingRight ? Vector2.right : Vector2.left, attackDistance, ~(1 << this.gameObject.layer) | 1 << 2);
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
            if(isBossEnemy)
            {
                if (Player.transform.localScale.x == 1)
                {
                    switch (Player.GetComponent<PlayerTakeCover>().coverBeTakenDefine)
                    {
                        case 1:
                            createBox1 = Instantiate(blackBody, player.transform.position, Quaternion.identity);
                            if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                                boss.GetComponent<BossCreateEnemy>().box1 = createBox1;
                            else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                                boss.GetComponent<BossCreateEnemy>().box2 = createBox1;
                            else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                                boss.GetComponent<BossCreateEnemy>().box3 = createBox1;
                            break;
                        case 2:
                            createBox1 = Instantiate(whiteBody, player.transform.position, Quaternion.identity);
                            if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                                boss.GetComponent<BossCreateEnemy>().box1 = createBox1;
                            else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                                boss.GetComponent<BossCreateEnemy>().box2 = createBox1;
                            else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                                boss.GetComponent<BossCreateEnemy>().box3 = createBox1;
                            break;
                        case 3:
                            createBox1 = Instantiate(goldenBody, player.transform.position, Quaternion.identity);
                            if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                                boss.GetComponent<BossCreateEnemy>().box1 = createBox1;
                            else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                                boss.GetComponent<BossCreateEnemy>().box2 = createBox1;
                            else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                                boss.GetComponent<BossCreateEnemy>().box3 = createBox1;
                            break;
                        default:
                            break;
                    }
                }
                else if (Player.transform.localScale.x == -1)
                {
                    switch (Player.GetComponent<PlayerTakeCover>().coverBeTakenDefine)
                    {
                        case 1:
                            createBox2 = Instantiate(blackBody, player.transform.position, Quaternion.Euler(0, 0, 0));
                            createBox2.transform.localScale = new Vector3(-1, 1, 1);
                            if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                                boss.GetComponent<BossCreateEnemy>().box1 = createBox2;
                            else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                                boss.GetComponent<BossCreateEnemy>().box2 = createBox2;
                            else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                                boss.GetComponent<BossCreateEnemy>().box3 = createBox2;
                            break;
                        case 2:
                            createBox2 = Instantiate(whiteBody, player.transform.position, Quaternion.Euler(0, 0, 0));
                            createBox2.transform.localScale = new Vector3(-1, 1, 1);
                            if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                                boss.GetComponent<BossCreateEnemy>().box1 = createBox2;
                            else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                                boss.GetComponent<BossCreateEnemy>().box2 = createBox2;
                            else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                                boss.GetComponent<BossCreateEnemy>().box3 = createBox2;
                            break;
                        case 3:
                            createBox2 = Instantiate(goldenBody, player.transform.position, Quaternion.Euler(0, 0, 0));
                            createBox2.transform.localScale = new Vector3(-1, 1, 1);
                            if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                                boss.GetComponent<BossCreateEnemy>().box1 = createBox2;
                            else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                                boss.GetComponent<BossCreateEnemy>().box2 = createBox2;
                            else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                                boss.GetComponent<BossCreateEnemy>().box3 = createBox2;
                            break;
                        default:
                            break;
                    }
                }
                Player.GetComponent<PlayerController>().jumpTime = 1;
                Player.GetComponent<PlayerController>().isJumpButtonDown = false;
                Destroy(Player.GetComponent<PlayerTakeCover>().nowSpring);
                Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                Player.GetComponent<PlayerTakeCover>().coverBeTakenDefine = 0;
                if (Player.transform.localScale.x == 1)
                    Player.transform.position = new Vector3(Player.transform.position.x - Player.GetComponent<PlayerTakeCover>().deltaX, Player.transform.position.y - Player.GetComponent<PlayerTakeCover>().deltaY, Player.transform.position.z);
                else if (Player.transform.localScale.x == -1)
                    Player.transform.position = new Vector3(Player.transform.position.x + Player.GetComponent<PlayerTakeCover>().deltaX, Player.transform.position.y - Player.GetComponent<PlayerTakeCover>().deltaY, Player.transform.position.z);
                Player.GetComponent<PlayerController>().isTakingCover = false;
                Player.GetComponent<CapsuleCollider2D>().enabled = true;
                Player.transform.GetChild(2).gameObject.SetActive(false);
                Player.transform.GetChild(0).gameObject.SetActive(true);
                Player.GetComponent<Animator>().runtimeAnimatorController = Player.transform.GetComponent<PlayerTakeCover>().playerAnim;
                Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                player.GetComponentInParent<Animator>().SetBool("IsBroken", true);
                player.GetComponentInParent<PlayerController>().jumpTime = 1;
                player.GetComponentInParent<PlayerController>().isJumpButtonDown = false;
                Destroy(player.GetComponentInParent<PlayerTakeCover>().nowSpring);
                player.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
    void ShutDown()
    {
        this.GetComponent<EnemyDestory>().DestroyWithBody();
    }
    private void OnDestroy()
    {
        if(this.GetComponent<MoveEnemyWarningUI>().thisWarningBar != null)
        {
            Destroy(this.GetComponent<MoveEnemyWarningUI>().thisWarningBar);
        }
    }
}
