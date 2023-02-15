using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorWave : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public GameObject goldenBody;
    public GameObject whiteBody;
    public GameObject blackBody;
    public GameObject createBox1;
    public GameObject createBox2;
    public GameObject boss;
    private void Start()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        player = GameObject.Find("Player");
        boss = GameObject.Find("King");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.transform.parent != null)
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
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().isHurt = true;
            collision.GetComponent<PlayerHurtHarmless>().allowHurt = false;
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(-collision.GetComponent<PlayerEnemyInterface>().bounceSpeed, 0);
            collision.GetComponent<PlayerEnemyInterface>().timerBegin = true;
            Physics2D.IgnoreCollision(collision, this.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(collision.transform.GetChild(0).GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());
        }
    }
    
}
