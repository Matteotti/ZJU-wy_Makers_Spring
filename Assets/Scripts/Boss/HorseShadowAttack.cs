using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseShadowAttack : MonoBehaviour
{
    public float angleMax;
    public float speed;
    public float rotateSpeed;
    public float disappearSpeed;
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject goldenBody;
    public GameObject whiteBody;
    public GameObject blackBody;
    public GameObject createBox1;
    public GameObject createBox2;
    public GameObject boss;
    public bool disappear;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        boss = GameObject.Find("King");
    }
    private void Update()
    {
        if (this.transform.rotation.eulerAngles.z < angleMax)
        {
            transform.Rotate(0, 0, rotateSpeed);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = Vector2.left * speed;
        }
        if (disappear)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, this.GetComponent<SpriteRenderer>().color.a - disappearSpeed);
            if (this.GetComponent<SpriteRenderer>().color.a < disappearSpeed)
                Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && collision.collider.transform.parent != null)
        {
            if (player.transform.localScale.x == 1)
            {
                switch (player.GetComponent<PlayerTakeCover>().coverBeTakenDefine)
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
            else if (player.transform.localScale.x == -1)
            {
                switch (player.GetComponent<PlayerTakeCover>().coverBeTakenDefine)
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
            player.GetComponent<PlayerController>().jumpTime = 1;
            player.GetComponent<PlayerController>().isJumpButtonDown = false;
            Destroy(player.GetComponent<PlayerTakeCover>().nowSpring);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            player.GetComponent<PlayerTakeCover>().coverBeTakenDefine = 0;
            if (player.transform.localScale.x == 1)
                player.transform.position = new Vector3(player.transform.position.x - player.GetComponent<PlayerTakeCover>().deltaX, player.transform.position.y - player.GetComponent<PlayerTakeCover>().deltaY, player.transform.position.z);
            else if (player.transform.localScale.x == -1)
                player.transform.position = new Vector3(player.transform.position.x + player.GetComponent<PlayerTakeCover>().deltaX, player.transform.position.y - player.GetComponent<PlayerTakeCover>().deltaY, player.transform.position.z);
            player.GetComponent<PlayerController>().isTakingCover = false;
            player.GetComponent<CapsuleCollider2D>().enabled = true;
            player.transform.GetChild(2).gameObject.SetActive(false);
            player.transform.GetChild(0).gameObject.SetActive(true);
            player.GetComponent<Animator>().runtimeAnimatorController = player.transform.GetComponent<PlayerTakeCover>().playerAnim;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), this.GetComponent<BoxCollider2D>());
            if (createBox1 != null)
                Physics2D.IgnoreCollision(createBox1.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());
            if (createBox2 != null)
                Physics2D.IgnoreCollision(createBox2.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());
        }
        else if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerController>().isHurt = true;
            collision.collider.GetComponent<PlayerHurtHarmless>().allowHurt = false;
            collision.collider.GetComponent<Rigidbody2D>().velocity = new Vector2(-collision.collider.GetComponent<PlayerEnemyInterface>().bounceSpeed, 0);
            collision.collider.GetComponent<PlayerEnemyInterface>().timerBegin = true;
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(collision.collider.transform.GetChild(0).GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());
        }
        else if (collision.collider.CompareTag("PlayerFeet"))
        {
            disappear = true;
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(collision.collider.transform.parent.GetComponent<CapsuleCollider2D>(), this.GetComponent<BoxCollider2D>());
            collision.collider.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2);
        }
    }
}
