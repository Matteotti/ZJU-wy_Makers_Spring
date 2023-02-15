using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdiotAttack : MonoBehaviour
{
    public GameObject littleBoss;
    void HitEnemy()
    {
        //Debug.Log("HIT");
    }
    void StopAttack()
    {
        this.GetComponent<Animator>().SetBool("IsAttacking", false);
    }
    void TryHitPlayer()
    {
        littleBoss.GetComponent<BossControl>().HP -= 2;
        littleBoss.GetComponent<BossControl>().blink = true;
        littleBoss.GetComponent<BossStateMachine>().isHurt = true;
    }
}
