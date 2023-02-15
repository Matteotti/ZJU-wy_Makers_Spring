using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public Animator animator;
    public bool isAngry;
    public bool isTired;
    public bool isEnemyLeft;
    public bool isHurt;
    public bool allowEnemyEmpty;
    public int loopNum;
    public int bossHP;
    public int integerCountera;
    public float counter;
    public float enemyLeftTime;
    public float loop1HorseTime;
    public float loop2HitFloorTime;
    private void Start()
    {
        animator = GetComponent<Animator>();
        counter = 0;
    }
    private void Update()
    {
        if (this.GetComponent<BossCreateEnemy>().enemy1 == null && this.GetComponent<BossCreateEnemy>().enemy2 == null && this.GetComponent<BossCreateEnemy>().enemy3 == null)
        {
            if (loopNum == 0 && allowEnemyEmpty)
            {
                loopNum = 1;
                counter = 0;
            }
            if (allowEnemyEmpty)
                isEnemyLeft = false;
        }
        else
        {
            isEnemyLeft = true;
            allowEnemyEmpty = true;
            //Debug.Log("ALLOW");
        }
        if(isHurt)
        {
            if(loopNum == 1)
            {
                isEnemyLeft = true;
                allowEnemyEmpty = false;
                loopNum = 2;
                counter = 0;
            }
            isHurt = false;
            isTired = false;
        }
        if(isAngry && loopNum != 3)
        {
            loopNum = 3;
            counter = 0;
        }
        if (loopNum == 0)
        {
            if(counter == 0)
            {
                animator.SetBool("IsHorse", true);
                if(integerCountera == 0)
                {
                    integerCountera++;
                    Invoke("CreateEnemy", 3);
                }
            }
            if(isEnemyLeft)
            {
                counter += Time.deltaTime;
            }
            else
            {
                counter = 0.01f;
            }
            if(counter > loop1HorseTime)
            {
                counter = 0;
            }
        }
        else if(loopNum == 1)
        {
            if(counter < loop2HitFloorTime && !isTired)
            {
                counter += Time.deltaTime;
            }
            else if(!isTired)
            {
                animator.SetBool("IsHitFloor", true);
                isTired = true;
                counter = 0;
            }
        }
        else if (loopNum == 2)
        {
            if(counter == 0)
            {
                animator.SetBool("RespawnEnemy", true);
                //马上horse = true;
            }
            if(isEnemyLeft)
            {
                counter += Time.deltaTime;
            }
            else
            {
                animator.SetBool("IsHitFloor", true);
                isTired = true;
                loopNum = 1;
            }
            if(counter > loop2HitFloorTime && isEnemyLeft)
            {
                counter = 0.001f;
                animator.SetBool("IsHorse", true);
            }
        }
        else if (loopNum == 3)
        {
            if(counter == 0)
            {
                animator.SetBool("RespawnEnemy", true);//马上horse = true;
                bossHP = this.GetComponent<BossControl>().HP;
            }
            if(!isEnemyLeft && counter <= 5)
            {
                counter += Time.deltaTime;
            }
            else if(isEnemyLeft)
            {
                counter = 0.01f;
            }
            if(counter > 5 && bossHP != this.GetComponent<BossControl>().HP && counter < 6)
            {
                animator.SetBool("IsHitFloor", true);
                counter = 7;
            }
        }
        if(this.GetComponent<BossControl>().HP == 0)
        {
            loopNum = 4;
            //end
        }
    }
    void StartHorse()
    {
        this.GetComponent<BossControl>().horseAttack = true;
    }
    void ExitHorse()
    {
        animator.SetBool("IsHorse", false);
    }
    void StartRespawnEnemy()
    {
        this.GetComponent<BossControl>().rebornEnemy = true;
    }
    void ExitRespawnEnemy()
    {
        animator.SetBool("RespawnEnemy", false);
        Invoke("StartHorse", 1.5f);
    }
    void StartCreateEnemy()
    {
        this.GetComponent<BossControl>().createEnemy = true;
    }
    void ExitCreateEnemy()
    {
        animator.SetBool("IsCreateEnemy", false);
    }
    void StartHitGround()
    {
        this.GetComponent<BossControl>().hitFloor = true;
    }
    void ExitHitGround()
    {
        animator.SetBool("IsHitFloor", false);
        if(loopNum == 3)
        {
            Invoke("ResetCounter", 8);
        }
    }
    void CreateEnemy()
    {
        animator.SetBool("IsCreateEnemy", true);
    }
    void ResetCounter()
    {
        counter = 0;
    }
}