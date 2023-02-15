using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyRaiseAlarm : MonoBehaviour
{
    public float minStopperLeft;
    public float maxStopperRight;
    public float platformPosLeft;
    public float platformPosRight;
    public float platformPosY;
    public int coverDefine;
    public GameObject hitEnemy;
    public Camera secondCamera;
    public GameObject secondCameraImage;
    private void Start()
    {
        secondCamera = GameObject.Find("Second Camera").GetComponent<Camera>();
    }
    public void RaiseAlarm(GameObject enemy, int playerCoverDefine)
    {
        // 1 - black 2 - white 3 - golden
        float counter;
        RaycastHit2D[] left = Physics2D.RaycastAll(enemy.transform.position, Vector2.left);
        RaycastHit2D[] right = Physics2D.RaycastAll(enemy.transform.position, Vector2.right);
        SortHits(left);
        SortHits(right);
        counter = 0;
        foreach (RaycastHit2D l in left)
        {
            if (l.collider.CompareTag("Stopper"))
            {
                counter++;
            }
            if (counter == 3)
            {
                minStopperLeft = l.transform.position.x;
            }
        }
        foreach (RaycastHit2D l in left)
        {
            if (l.collider.CompareTag("Enemy") && l.transform.position.x >= minStopperLeft)
            {
                SeeThroughPlayer(l.collider.gameObject, playerCoverDefine);
                if((l.collider.GetComponent<HostileEnemyMove>() != null && l.collider.GetComponent<HostileEnemyMove>().enemyDefine != playerCoverDefine)||(l.collider.GetComponent<HostileStaticEnemy>() != null && l.collider.GetComponent<HostileStaticEnemy>().enemyDefine != playerCoverDefine))
                    l.collider.GetComponent<EnemyRaiseAlarm>().AttackSameColor(playerCoverDefine);
                //Debug.Log(l.transform.position.x);
                //Debug.Log(l.collider);
            }
        }
        counter = 0;
        foreach (RaycastHit2D r in right)
        {
            if (r.collider.CompareTag("Stopper"))
            {
                counter++;
            }
            if(counter == 1)
            {
                platformPosLeft = r.transform.position.x;
            }
            else if(counter == 2)
            {
                maxStopperRight = r.transform.position.x;
                platformPosY = r.transform.position.y;
            }
            else if (counter == 3)
            {
                platformPosRight = r.transform.position.x;
            }
        }
        foreach (RaycastHit2D r in right)
        {
            if (r.collider.CompareTag("Enemy") && r.transform.position.x < maxStopperRight)
            {
                SeeThroughPlayer(r.collider.gameObject, playerCoverDefine);
                if ((r.collider.GetComponent<HostileEnemyMove>() != null && r.collider.GetComponent<HostileEnemyMove>().enemyDefine != playerCoverDefine) || (r.collider.GetComponent<HostileStaticEnemy>() != null && r.collider.GetComponent<HostileStaticEnemy>().enemyDefine != playerCoverDefine))
                    r.collider.GetComponent<EnemyRaiseAlarm>().AttackSameColor(playerCoverDefine);
                //Debug.Log(playerCoverDefine);
                //Debug.Log(r.transform.position.x);
                //Debug.Log(r.collider);
            }
        }
        secondCamera.transform.position = new Vector3((platformPosLeft + platformPosRight) / 2, platformPosY, secondCamera.transform.position.z);
        secondCameraImage.SetActive(true); 
        secondCameraImage.SendMessage("Disable", SendMessageOptions.DontRequireReceiver);
    }
    //private void Update()
    //{
    //    float counter;
    //    RaycastHit2D[] left = Physics2D.RaycastAll(this.transform.position, Vector2.left, 100);
    //    RaycastHit2D[] right = Physics2D.RaycastAll(this.transform.position, Vector2.right, 100);
    //    SortHits(left);
    //    SortHits(right);
    //    counter = 0;
    //    foreach (RaycastHit2D l in left)
    //    {
    //        if (l.collider.CompareTag("Stopper"))
    //        {
    //            counter++;
    //        }
    //        if (counter == 3)
    //        {
    //            minStopperLeft = l.transform.position.x;
    //        }
    //    }
    //    foreach (RaycastHit2D l in left)
    //    {
    //        if (l.collider.CompareTag("Enemy") && l.transform.position.x > minStopperLeft)
    //        {
    //            Debug.Log(l.collider);
    //        }
    //    }
    //    counter = 0;
    //    foreach (RaycastHit2D r in right)
    //    {
    //        if (r.collider.CompareTag("Stopper"))
    //        {
    //            counter++;
    //        }
    //        if (counter == 3)
    //        {
    //            maxStopperRight = r.transform.position.x;
    //        }
    //    }
    //    foreach (RaycastHit2D r in right)
    //    {
    //        if (r.collider.CompareTag("Enemy") && r.transform.position.x < maxStopperRight)
    //        {
    //            Debug.Log(r.collider);
    //        }
    //    }
    //}
    public static void SortHits(RaycastHit2D[] hits)
    {
        Array.Sort(hits, HitComparison);
    }
    private static int HitComparison(RaycastHit2D a, RaycastHit2D b)
    {
        if (a.distance <= b.distance)
        {
            return -1;
        }
        return 1;
    }
    void SeeThroughPlayer(GameObject enemy, int playerCoverDefine)
    {
        if (enemy.GetComponent<HostileEnemyMove>() != null)
        {
            if (playerCoverDefine == 1)
            {
                enemy.GetComponent<HostileEnemyMove>().allowBlack = false;
            }
            else if (playerCoverDefine == 2)
            {
                enemy.GetComponent<HostileEnemyMove>().allowWhite = false;
            }
            else if (playerCoverDefine == 3)
            {
                enemy.GetComponent<HostileEnemyMove>().allowGolden = false;
            }
        }
        if (enemy.GetComponent<HostileStaticEnemy>() != null)
        {
            if (playerCoverDefine == 1)
            {
                enemy.GetComponent<HostileStaticEnemy>().allowBlack = false;
            }
            else if (playerCoverDefine == 2)
            {
                enemy.GetComponent<HostileStaticEnemy>().allowWhite = false;
                //Debug.Log("@");
            }
            else if (playerCoverDefine == 3)
            {
                enemy.GetComponent<HostileStaticEnemy>().allowGolden = false;
            }
        }
    }
    void AttackSameColor(int playerCoverDefine)
    {
        coverDefine = playerCoverDefine;
        //Debug.Log(this.name + this.transform.position.x);
        //确认攻击目标void CheckWhichAllow()
        RaycastHit2D left = Physics2D.Raycast(this.transform.position, Vector2.left, float.PositiveInfinity, 1 << 3);
        RaycastHit2D right = Physics2D.Raycast(this.transform.position, Vector2.right, float.PositiveInfinity, 1 << 3);
        RaycastHit2D[] leftEnemy = Physics2D.RaycastAll(this.transform.position, Vector2.left, this.transform.position.x - left.point.x, 1 << 9);
        RaycastHit2D[] rightEnemy = Physics2D.RaycastAll(this.transform.position, Vector2.right, right.point.x - this.transform.position.x, 1 << 9);
        //Debug.DrawRay(this.transform.position, Vector2.left * (this.transform.position.x - left.point.x));
        //Debug.DrawRay(this.transform.position, Vector2.right * (right.point.x - this.transform.position.x));
        foreach (RaycastHit2D hit in leftEnemy)
        {
            if (hit.collider.GetComponent<HostileEnemyMove>() != null && hit.collider.GetComponent<HostileEnemyMove>().enemyDefine == playerCoverDefine)
            {
                if (this.GetComponent<HostileEnemyMove>() != null)
                {
                    this.GetComponent<HostileEnemyMove>().enemyPosX = hit.transform.position.x;
                    this.GetComponent<HostileEnemyMove>().attackEnemy = true;
                    hitEnemy = hit.collider.gameObject;
                    goto a;
                }
                else
                {
                    this.GetComponent<HostileEnemyMove>().attackEnemy = false;
                }
                if (this.GetComponent<HostileStaticEnemy>() != null)
                {
                    this.GetComponent<HostileStaticEnemy>().enemyPosX = hit.transform.position.x;
                    this.GetComponent<HostileStaticEnemy>().attackEnemy = true;
                    hitEnemy = hit.collider.gameObject;
                    goto a;
                }
                else
                {
                    this.GetComponent<HostileStaticEnemy>().attackEnemy = false;
                }
            }
            if (hit.collider.GetComponent<HostileStaticEnemy>() != null && hit.collider.GetComponent<HostileStaticEnemy>().enemyDefine == playerCoverDefine)
            {
                if (this.GetComponent<HostileEnemyMove>() != null)
                {
                    this.GetComponent<HostileEnemyMove>().enemyPosX = hit.transform.position.x;
                    this.GetComponent<HostileEnemyMove>().attackEnemy = true;
                    hitEnemy = hit.collider.gameObject;
                    goto a;
                }
                else if (this.GetComponent<HostileEnemyMove>() != null)
                {
                    this.GetComponent<HostileEnemyMove>().attackEnemy = false;
                }
                if (this.GetComponent<HostileStaticEnemy>() != null)
                {
                    this.GetComponent<HostileStaticEnemy>().enemyPosX = hit.transform.position.x;
                    this.GetComponent<HostileStaticEnemy>().attackEnemy = true;
                    hitEnemy = hit.collider.gameObject;
                    goto a;
                }
                else if (this.GetComponent<HostileStaticEnemy>() != null)
                {
                    this.GetComponent<HostileStaticEnemy>().attackEnemy = false;
                }
            }
        }
        foreach (RaycastHit2D hit in rightEnemy)
        {
            if (hit.collider.GetComponent<HostileEnemyMove>() != null && hit.collider.GetComponent<HostileEnemyMove>().enemyDefine == playerCoverDefine)
            {
                if (this.GetComponent<HostileEnemyMove>() != null)
                {
                    this.GetComponent<HostileEnemyMove>().enemyPosX = hit.transform.position.x;
                    this.GetComponent<HostileEnemyMove>().attackEnemy = true;
                    hitEnemy = hit.collider.gameObject;
                    goto a;
                }
                if (this.GetComponent<HostileStaticEnemy>() != null)
                {
                    this.GetComponent<HostileStaticEnemy>().enemyPosX = hit.transform.position.x;
                    this.GetComponent<HostileStaticEnemy>().attackEnemy = true;
                    hitEnemy = hit.collider.gameObject;
                    goto a;
                }
            }
            else
            {
                if (this.GetComponent<HostileEnemyMove>() != null)
                {
                    this.GetComponent<HostileEnemyMove>().attackEnemy = false;
                }
                if (this.GetComponent<HostileStaticEnemy>() != null)
                {
                    this.GetComponent<HostileStaticEnemy>().attackEnemy = false;
                }
            }
            if (hit.collider.GetComponent<HostileStaticEnemy>() != null && hit.collider.GetComponent<HostileStaticEnemy>().enemyDefine == playerCoverDefine)
            {
                if (this.GetComponent<HostileEnemyMove>() != null)
                {
                    this.GetComponent<HostileEnemyMove>().enemyPosX = hit.transform.position.x;
                    this.GetComponent<HostileEnemyMove>().attackEnemy = true;
                    hitEnemy = hit.collider.gameObject;
                    goto a;
                }
                if (this.GetComponent<HostileStaticEnemy>() != null)
                {
                    this.GetComponent<HostileStaticEnemy>().enemyPosX = hit.transform.position.x;
                    this.GetComponent<HostileStaticEnemy>().attackEnemy = true;
                    hitEnemy = hit.collider.gameObject;
                    goto a;
                }
            }
            else
            {
                if (this.GetComponent<HostileEnemyMove>() != null)
                {
                    this.GetComponent<HostileEnemyMove>().attackEnemy = false;
                }
                if (this.GetComponent<HostileStaticEnemy>() != null)
                {
                    this.GetComponent<HostileStaticEnemy>().attackEnemy = false;
                }
            }
        }
    a:;
    }
    void HitEnemy()
    {
        if((this.GetComponent<HostileEnemyMove>() != null && this.GetComponent<HostileEnemyMove>().attackEnemy) ||(this.GetComponent<HostileStaticEnemy>() != null && this.GetComponent<HostileStaticEnemy>().attackEnemy))
        {
            if (this.GetComponent<HostileEnemyMove>() != null)
            {
                this.GetComponent<HostileEnemyMove>().attackEnemy = false;
            }
            else if (this.GetComponent<HostileStaticEnemy>() != null)
            {
                this.GetComponent<HostileStaticEnemy>().attackEnemy = false;
            }
            if (hitEnemy != null)
            {
                hitEnemy.GetComponent<Animator>().SetBool("IsBroken", true);
            }
            //Debug.Log("HitEnemy");
            AttackSameColor(coverDefine);
        }
    }
}

