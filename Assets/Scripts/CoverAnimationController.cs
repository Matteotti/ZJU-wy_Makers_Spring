using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverAnimationController : MonoBehaviour
{
    public Animator animator;
    public float enemyCheckVertical;
    public float enemyCheckDistance;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    //private void Update()
    //{
    //    Debug.DrawRay(transform.position + new Vector3(0, enemyCheckVertical, 0), this.transform.localScale.x * Vector2.left * enemyCheckDistance);
    //    Debug.DrawRay(transform.position + new Vector3(0, 0, 0), this.transform.localScale.x * Vector2.left * enemyCheckDistance);
    //    Debug.DrawRay(transform.position + new Vector3(0, -enemyCheckVertical, 0), this.transform.localScale.x * Vector2.left * enemyCheckDistance);
    //}
    public void HitEnemy()
    {

        bool hitEnemy = false;
        GameObject enemy = null;
        RaycastHit2D tryHitPlayerCheck = Physics2D.Raycast(transform.position + new Vector3(0, enemyCheckVertical, 0), this.transform.localScale.x * Vector2.left, enemyCheckDistance, ~(1 << this.gameObject.layer | 1 << 2));
        if (tryHitPlayerCheck.collider != null && (tryHitPlayerCheck.collider.CompareTag("Enemy") || tryHitPlayerCheck.collider.CompareTag("EnemyBody")))
        {
            hitEnemy = true;
            enemy = tryHitPlayerCheck.collider.gameObject;
        }
        tryHitPlayerCheck = Physics2D.Raycast(transform.position + new Vector3(0, 0, 0), this.transform.localScale.x * Vector2.left, enemyCheckDistance, ~(1 << this.gameObject.layer | 1 << 2));
        if (tryHitPlayerCheck.collider != null && (tryHitPlayerCheck.collider.CompareTag("Enemy") || tryHitPlayerCheck.collider.CompareTag("EnemyBody")))
        {
            hitEnemy = true;
            enemy = tryHitPlayerCheck.collider.gameObject;
        }
        tryHitPlayerCheck = Physics2D.Raycast(transform.position + new Vector3(0, -enemyCheckVertical, 0), this.transform.localScale.x * Vector2.left, enemyCheckDistance, ~(1 << this.gameObject.layer | 1 << 2));
        if (tryHitPlayerCheck.collider != null && (tryHitPlayerCheck.collider.CompareTag("Enemy") || tryHitPlayerCheck.collider.CompareTag("EnemyBody")))
        {
            hitEnemy = true;
            enemy = tryHitPlayerCheck.collider.gameObject;
        }
        if(hitEnemy)
        {
            if(this.GetComponent<PlayerController>().boss)
            {
                if(enemy.name == "King")
                {
                    enemy.GetComponent<BossControl>().HP -= 1;
                    enemy.GetComponent<BossControl>().blink = true;
                    enemy.GetComponent<BossStateMachine>().isHurt = true;
                }
                else
                {
                    enemy.GetComponent<Animator>().SetBool("IsShutDown", true);
                    enemy.transform.GetChild(0).gameObject.SetActive(false);
                    enemy.SendMessage("Idiot", this.GetComponent<PlayerTakeCover>().coverBeTakenDefine, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                enemy.GetComponent<Animator>().SetBool("IsBroken", true);
                if (enemy.GetComponent<EnemyRaiseAlarm>() != null)
                    enemy.GetComponent<EnemyRaiseAlarm>().RaiseAlarm(enemy, this.GetComponent<PlayerTakeCover>().coverBeTakenDefine);
                if (enemy.GetComponent<BoxCollider2D>() != null)
                    Physics2D.IgnoreCollision(this.transform.GetChild(2).GetComponent<BoxCollider2D>(), enemy.GetComponent<BoxCollider2D>());
                if (enemy.GetComponent<CapsuleCollider2D>() != null)
                    Physics2D.IgnoreCollision(this.transform.GetChild(2).GetComponent<BoxCollider2D>(), enemy.GetComponent<CapsuleCollider2D>());
            }
        }
    }
    public void TryHitPlayer()
    {
        //dunno
        //Debug.Log("DUNNO");
    }
    public void StopIdle()
    {
        animator.SetBool("IsIdle", false);
    }
    public void StopAttack()
    {
        animator.SetBool("IsAttacking", false);
        //Debug.Log("STOP");
    }
    public void Destroy()
    {
        this.GetComponent<PlayerTakeCover>().coverBeTakenDefine = 0;
        this.transform.position = new Vector3(this.transform.position.x - this.transform.GetComponent<PlayerTakeCover>().deltaX, this.transform.position.y, this.transform.position.z);
        this.GetComponent<PlayerController>().isTakingCover = false;
        this.GetComponent<CapsuleCollider2D>().enabled = true;
        this.transform.GetChild(2).gameObject.SetActive(false);
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.GetComponent<Animator>().runtimeAnimatorController = this.transform.GetComponent<PlayerTakeCover>().playerAnim;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<PlayerHurtHarmless>().allowHurt = false;
    }
}
