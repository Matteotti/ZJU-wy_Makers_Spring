using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    public GameObject horse;
    public bool isAngry;
    public bool horseAttack;
    public bool hitFloor;
    public bool createEnemy;
    public bool rebornEnemy;
    public bool blink;
    public int HP = 10;
    private void Update()
    {
        if (horseAttack)
        {
            if (isAngry)
            {
                horse.GetComponent<HorseMatrixAttack>().horse.GetComponent<HorseShadowAttack>().speed = 20;
            }
            else
            {
                horse.GetComponent<HorseMatrixAttack>().horse.GetComponent<HorseShadowAttack>().speed = 10;
            }
            horse.GetComponent<HorseMatrixAttack>().attack = true;
            horseAttack = false;
        }
        if (hitFloor)
        {
            this.GetComponent<BossHitFloor>().attack = true;
            hitFloor = false;
        }
        if (createEnemy)
        {
            this.GetComponent<BossCreateEnemy>().attack = true;
            createEnemy = false;
        }
        if (rebornEnemy)
        {
            this.GetComponent<BossRebornEnemy>().reborn = true;
            rebornEnemy = false;
        }
        if (blink)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
            Invoke("Recover", 0.5f);
            blink = false;
        }
        //¿ñ±©ÅÐ¶Ï
        if (HP <= 4)
        {
            isAngry = true;
            this.GetComponent<BossStateMachine>().isAngry = true;
        }
    }
    void Recover()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
