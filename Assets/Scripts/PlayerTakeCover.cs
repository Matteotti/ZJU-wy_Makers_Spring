using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeCover : MonoBehaviour
{
    public bool isAllowTakeCover;
    public bool transparent;
    public bool recover;
    public bool allowQuit;
    public GameObject cover;
    public GameObject blackBody;
    public GameObject whiteBody;
    public GameObject goldenBody;
    public GameObject player;
    public GameObject playersSpringAnim;
    public GameObject playersSpringDisappearAnim;
    public GameObject playersSpring;
    public GameObject nowSpring;
    public GameObject createBox;
    public GameObject boss;
    public RuntimeAnimatorController playerAnim;
    public RuntimeAnimatorController blackAnim;
    public RuntimeAnimatorController whiteAnim;
    public RuntimeAnimatorController goldenAnim;
    public int coverDefineNum;
    public int coverBeTakenDefine;
    public float transparentSpeed;
    public float deltaY;
    public float deltaX;
    public float rightSpringDeltaY;
    public float rightSpringDeltaX;
    public float leftSpringDeltaY;
    public float leftSpringDeltaX;
    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isAllowTakeCover && !player.GetComponent<PlayerController>().isTakingCover && cover.GetComponent<BoxControll>().coverDefineNum != 0)
            {
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                if (cover.transform.position.x > player.transform.position.x)
                    player.transform.localScale = new Vector3(1, player.transform.localScale.y, player.transform.localScale.z);
                else if (cover.transform.position.x < player.transform.position.x)
                    player.transform.localScale = new Vector3(-1, player.transform.localScale.y, player.transform.localScale.z);
                if (cover.transform.position.x > player.transform.position.x)
                {
                    Instantiate(playersSpringAnim, cover.transform.position + new Vector3(leftSpringDeltaX, leftSpringDeltaY, 0), Quaternion.identity);
                }
                else if (cover.transform.position.x < player.transform.position.x)
                {
                    var a = Instantiate(playersSpringAnim, cover.transform.position + new Vector3(rightSpringDeltaX, rightSpringDeltaY, 0), Quaternion.identity);
                    a.transform.localScale = new Vector3(-a.transform.localScale.x, a.transform.localScale.y, a.transform.localScale.z);
                }
                this.GetComponent<Animator>().SetBool("IsTakeCover", true);
                transparent = true;
            }
            else if (player.GetComponent<PlayerController>().isTakingCover && allowQuit)
            {
                QuitCover();
            }
        }
        if (transparent && this.GetComponent<SpriteRenderer>().color.a > 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, this.GetComponent<SpriteRenderer>().color.a - transparentSpeed);
        }
        else if (recover && this.GetComponent<SpriteRenderer>().color.a < 1)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, this.GetComponent<SpriteRenderer>().color.a + transparentSpeed);
        }
    }
    void TakeCover()
    {
        if (cover.transform.position.x > player.transform.position.x)
        {
            player.transform.position = new Vector3(player.transform.position.x - deltaX, player.transform.position.y + deltaY, player.transform.position.z);
            nowSpring = Instantiate(playersSpring, cover.transform.position + new Vector3(rightSpringDeltaX - 0.1f, rightSpringDeltaY, 0), Quaternion.identity);
            nowSpring.transform.localScale = new Vector3(-nowSpring.transform.localScale.x, nowSpring.transform.localScale.y, nowSpring.transform.localScale.z);
            nowSpring.transform.SetParent(this.transform);
        }
        else if (cover.transform.position.x < player.transform.position.x)
        {
            player.transform.position = new Vector3(player.transform.position.x + deltaX, player.transform.position.y + deltaY, player.transform.position.z);
            nowSpring = Instantiate(playersSpring, cover.transform.position + new Vector3(leftSpringDeltaX + 0.1f, leftSpringDeltaY, 0), Quaternion.identity);
            nowSpring.transform.SetParent(this.transform);
        }
        player.transform.localScale = new Vector3(-player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
        player.GetComponent<PlayerController>().isTakingCover = true;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        player.transform.GetChild(2).gameObject.SetActive(true);
        player.transform.GetChild(0).gameObject.SetActive(false);
        coverBeTakenDefine = cover.GetComponent<BoxControll>().coverDefineNum;
        this.GetComponent<Animator>().SetBool("IsTakeCover", true);
        transparent = false;
        this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, 1);
        switch (cover.GetComponent<BoxControll>().coverDefineNum)
        {
            case 1:
                player.GetComponent<Animator>().runtimeAnimatorController = blackAnim;
                break;
            case 2:
                player.GetComponent<Animator>().runtimeAnimatorController = whiteAnim;
                break;
            case 3:
                player.GetComponent<Animator>().runtimeAnimatorController = goldenAnim;
                break;
            default:
                break;
        }
        Destroy(cover);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void QuitCover()
    {
        player.GetComponent<PlayerController>().isJumpButtonDown = false;
        if (this.transform.localScale.x == 1)
            switch (coverBeTakenDefine)
            {
                case 1:
                    createBox = Instantiate(blackBody, this.transform.position, Quaternion.identity);
                    if(player.GetComponent<PlayerController>().boss)
                    {
                        createBox.GetComponent<BoxControll>().isBossEnemyBody = true;
                        if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box1 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box2 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box3 = createBox;
                        }
                    }
                    break;
                case 2:
                    createBox = Instantiate(whiteBody, this.transform.position, Quaternion.identity);
                    if (player.GetComponent<PlayerController>().boss)
                    {
                        createBox.GetComponent<BoxControll>().isBossEnemyBody = true;
                        if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box1 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box2 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box3 = createBox;
                        }
                    }
                    break;
                case 3:
                    createBox = Instantiate(goldenBody, this.transform.position, Quaternion.identity);
                    if (player.GetComponent<PlayerController>().boss)
                    {
                        createBox.GetComponent<BoxControll>().isBossEnemyBody = true;
                        if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box1 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box2 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box3 = createBox;
                        }
                    }
                    break;
                default:
                    break;
            }
        else if (this.transform.localScale.x == -1)
            switch (coverBeTakenDefine)
            {
                case 1:
                    createBox = Instantiate(blackBody, this.transform.position, Quaternion.Euler(0, 0, 0));
                    createBox.transform.localScale = new Vector3(-1, 1, 1);
                    if (player.GetComponent<PlayerController>().boss)
                    {
                        createBox.GetComponent<BoxControll>().isBossEnemyBody = true;
                        if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box1 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box2 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box3 = createBox;
                        }
                    }
                    break;
                case 2:
                    createBox = Instantiate(whiteBody, this.transform.position, Quaternion.Euler(0, 0, 0));
                    createBox.transform.localScale = new Vector3(-1, 1, 1);
                    if (player.GetComponent<PlayerController>().boss)
                    {
                        createBox.GetComponent<BoxControll>().isBossEnemyBody = true;
                        if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box1 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box2 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box3 = createBox;
                        }
                    }
                    break;
                case 3:
                    createBox = Instantiate(goldenBody, this.transform.position, Quaternion.Euler(0, 0, 0));
                    createBox.transform.localScale = new Vector3(-1, 1, 1);
                    if (player.GetComponent<PlayerController>().boss)
                    {
                        createBox.GetComponent<BoxControll>().isBossEnemyBody = true;
                        if (boss.GetComponent<BossCreateEnemy>().box1 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box1 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box2 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box2 = createBox;
                        }
                        else if (boss.GetComponent<BossCreateEnemy>().box3 == null)
                        {
                            boss.GetComponent<BossCreateEnemy>().box3 = createBox;
                        }
                    }
                    break;
                default:
                    break;
            }
        Destroy(nowSpring);
        coverBeTakenDefine = 0;
        if (this.transform.localScale.x == 1)
            player.transform.position = new Vector3(player.transform.position.x - deltaX, player.transform.position.y - deltaY, player.transform.position.z);
        else if (this.transform.localScale.x == -1)
            player.transform.position = new Vector3(player.transform.position.x + deltaX, player.transform.position.y - deltaY, player.transform.position.z);
        player.GetComponent<PlayerController>().isTakingCover = false;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        player.transform.GetChild(2).gameObject.SetActive(false);
        player.transform.GetChild(0).gameObject.SetActive(true);
        player.GetComponent<Animator>().runtimeAnimatorController = playerAnim;
        player.transform.localScale = new Vector3(-player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
        player.GetComponent<Animator>().SetBool("IsQuitCover", true);
        QuitAnim();
    }
    void QuitAnim()
    {
        if (player.transform.position.x < createBox.transform.position.x)
        {
            Instantiate(playersSpringDisappearAnim, createBox.transform.position + new Vector3(leftSpringDeltaX, leftSpringDeltaY, 0), Quaternion.identity);
        }
        else if (player.transform.position.x > createBox.transform.position.x)
        {
            var a = Instantiate(playersSpringDisappearAnim, createBox.transform.position + new Vector3(rightSpringDeltaX, rightSpringDeltaY, 0), Quaternion.identity);
            a.transform.localScale = new Vector3(-a.transform.localScale.x, a.transform.localScale.y, a.transform.localScale.z);
        }
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        transparent = false;
        recover = true;
    }
    void QuitCoverAnim()
    {
        player.GetComponent<Animator>().SetBool("IsQuitCover", false);
    }
}
