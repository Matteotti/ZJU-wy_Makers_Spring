using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyInterface : MonoBehaviour
{
    public float bounceSpeed = 10;
    public float brokeBounceSpeed;
    public float bounceTime = 1;
    public bool timerBegin = false;
    public bool collided;
    public float counter;
    public float playerSpeedY;
    public float minAllowedSpeed;
    public GameObject player;
    public Animator animator;
    //public Animator enemyAnimator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // enemyAnimator = collision.gameObject.GetComponent<Animator>();
        collided = true;
        if (collision.collider.CompareTag("Enemy"))
        {
            if (collision.otherCollider.gameObject.CompareTag("Player"))
            {
                if (collision.otherCollider.transform.parent != null && collision.otherCollider.transform.parent.CompareTag("Player"))
                {

                }
                else if (collision.otherCollider.gameObject.GetComponent<PlayerHurtHarmless>().allowHurt && collision.otherCollider.transform.position.x <= collision.collider.transform.position.x)//player left
                {
                    collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(-bounceSpeed, collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity.y);
                    collision.otherCollider.transform.localScale = new Vector3(1, 1, 1);
                    collision.otherCollider.GetComponent<PlayerController>().isGrounded = false;
                    collision.otherCollider.gameObject.GetComponent<PlayerHurtHarmless>().allowHurt = false;
                    timerBegin = true;
                    animator.SetBool("IsHurt", true);
                    animator.SetBool("IsPlugging", false);

                }
                else if (collision.otherCollider.gameObject.GetComponent<PlayerHurtHarmless>().allowHurt && collision.otherCollider.transform.position.x > collision.collider.transform.position.x)//player right
                {
                    collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(bounceSpeed, collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity.y);
                    collision.otherCollider.transform.localScale = new Vector3(-1, 1, 1);
                    collision.otherCollider.GetComponent<PlayerController>().isGrounded = false;
                    collision.otherCollider.gameObject.GetComponent<PlayerHurtHarmless>().allowHurt = false;
                    timerBegin = true;
                    animator.SetBool("IsHurt", true);
                    animator.SetBool("IsPlugging", false);
                }
            }
            if (collision.otherCollider.gameObject.CompareTag("PlayerFeet") && playerSpeedY < -minAllowedSpeed && collision.collider.GetComponent<HostileEnemyMove>() != null && collision.collider.GetComponent<HostileEnemyMove>().isBossEnemy)
            {
                collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity.x, brokeBounceSpeed);
                collision.collider.GetComponent<HostileEnemyMove>().isBroken = true;
                if (collision.collider.GetComponent<MoveEnemyWarningUI>().thisWarningBar != null)
                {
                    Destroy(collision.collider.GetComponent<MoveEnemyWarningUI>().thisWarningBar);
                    collision.collider.GetComponent<MoveEnemyWarningUI>().enabled = false;
                }
                collision.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                collision.collider.GetComponent<HostileEnemyMove>().animator.SetBool("IsShutDown", true);
                collision.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider.transform.parent.GetComponent<CapsuleCollider2D>(), true);
            }
            else if (collision.otherCollider.gameObject.CompareTag("PlayerFeet") && playerSpeedY < -minAllowedSpeed)
            {
                //Debug.Log(playerSpeedY);
                //collision.collider.GetComponent<EnemyRaiseAlarm>().RaiseAlarm(collision.collider.gameObject, player.GetComponent<PlayerTakeCover>().coverBeTakenDefine);
                collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity.x, brokeBounceSpeed);
                collision.otherCollider.GetComponentInParent<PlayerController>().jumpTime = 2;
                Destroy(collision.collider.transform.GetChild(0).gameObject);
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider.transform.parent.GetComponent<CapsuleCollider2D>(), true);
                collision.collider.gameObject.GetComponent<Animator>().SetBool("IsBroken", true);
            }
            else if (collision.otherCollider.gameObject.CompareTag("PlayerFeet") && playerSpeedY >= -minAllowedSpeed)
            {
                collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity.x, brokeBounceSpeed);
            }
        }
        else if (collision.collider.CompareTag("GoodEnemy"))
        {
            if (collision.otherCollider.gameObject.CompareTag("PlayerFeet") && playerSpeedY < -minAllowedSpeed)
            {
                //Debug.Log(playerSpeedY);
                collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity.x, brokeBounceSpeed);
                collision.otherCollider.GetComponentInParent<PlayerController>().jumpTime = 2;
                Destroy(collision.collider.transform.GetChild(0).gameObject);
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider.transform.parent.GetComponent<CapsuleCollider2D>(), true);
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
                collision.collider.gameObject.GetComponent<Animator>().SetBool("IsBroken", true);
            }
            else if (collision.otherCollider.gameObject.CompareTag("PlayerFeet") && playerSpeedY >= -minAllowedSpeed)
            {
                collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(collision.otherCollider.GetComponentInParent<Rigidbody2D>().velocity.x, brokeBounceSpeed);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collided = false;
    }
    private void Update()
    {
        if (timerBegin)
        {
            counter += Time.deltaTime;
            this.GetComponent<PlayerController>().isHurt = true;
            if (counter >= bounceTime)
            {
                counter = 0;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                timerBegin = false;
            }
        }
        if (!collided)
        {
            playerSpeedY = player.GetComponent<Rigidbody2D>().velocity.y;
        }
    }
}
