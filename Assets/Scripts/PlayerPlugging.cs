using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlugging : MonoBehaviour
{
    public GameObject enemy, spring;
    public void StartPlugging()
    {
        //if(enemy.GetComponent<EnemyRaiseAlarm>() != null)
        //    enemy.GetComponent<EnemyRaiseAlarm>().RaiseAlarm(enemy, this.GetComponent<PlayerTakeCover>().coverBeTakenDefine);
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        if (enemy.GetComponent<Rigidbody2D>() != null)
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        if (enemy.GetComponent<HostileEnemyMove>() != null)
        {
            enemy.GetComponent<HostileEnemyMove>().isPlugging = true;
        }
        if (enemy.GetComponent<HostileStaticEnemy>() != null)
        {
            enemy.GetComponent<HostileStaticEnemy>().isPlugging = true;
        }
        Physics2D.IgnoreCollision(this.GetComponent<CapsuleCollider2D>(), enemy.GetComponent<CapsuleCollider2D>());
        Destroy(spring);
    }
    public void EndPlugging()
    {
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<PlayerController>().isPlugging = false;
        this.GetComponent<PlayerController>().animator.SetBool("IsPlugging", false);
        enemy.GetComponent<Animator>().SetBool("IsBroken", true);
    }
    public void Assign(GameObject that)
    {
        enemy = that.transform.parent.gameObject;
        spring = that;
    }
}
