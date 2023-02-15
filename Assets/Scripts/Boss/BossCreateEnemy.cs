using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreateEnemy : MonoBehaviour
{
    public bool attack;
    public float limitLeft;
    public float limitRight;
    public float enemyPos1;
    public float enemyPos2;
    public float enemyPos3;
    public float enemyPosY;
    public float shieldHorizontal;
    public float buttonHorizontal;
    public float buttonVertical;
    public GameObject whiteEnemy;
    public GameObject blackEnemy;
    public GameObject goldenEnemy;
    public GameObject enemy1;
    public GameObject box1;
    public GameObject enemy2;
    public GameObject box2;
    public GameObject enemy3;
    public GameObject box3;
    public GameObject shield;
    public GameObject button;
    private void Update()
    {
        if (attack)
        {
            CreateEnemy();
            attack = false;
        }
    }
    void CreateEnemy()
    {
        enemyPos1 = Random.Range(limitLeft, limitRight);
        enemyPos2 = Random.Range(limitLeft, limitRight);
        while (Mathf.Abs(enemyPos1 - enemyPos2) < 1)
        {
            enemyPos2 = Random.Range(limitLeft, limitRight);
        }
        enemy1 = Instantiate(goldenEnemy, new Vector2(enemyPos1, enemyPosY), Quaternion.identity);
        enemy1.GetComponent<HostileEnemyMove>().isBossEnemy = true;
        box1 = enemy1.transform.GetChild(1).gameObject;
        enemy2 = Instantiate(blackEnemy, new Vector2(enemyPos2, enemyPosY), Quaternion.identity);
        enemy2.GetComponent<HostileEnemyMove>().isBossEnemy = true;
        box2 = enemy2.transform.GetChild(1).gameObject;
        if (this.GetComponent<BossControl>().isAngry)
        {
            enemyPos3 = Random.Range(limitLeft, limitRight);
            while (Mathf.Abs(enemyPos3 - enemyPos2) < 1 || Mathf.Abs(enemyPos3 - enemyPos1) < 1)
            {
                enemyPos3 = Random.Range(limitLeft, limitRight);
            }
            enemy3 = Instantiate(whiteEnemy, new Vector2(enemyPos3, enemyPosY), Quaternion.identity);
            enemy3.GetComponent<HostileEnemyMove>().isBossEnemy = true;
            box3 = enemy2.transform.GetChild(1).gameObject;
            if (GameObject.Find("Button(Clone)") == null)
                Instantiate(button, transform.position + new Vector3(buttonHorizontal, buttonVertical, 0), Quaternion.identity);
            if (GameObject.Find("Shield(Clone)") == null)
                Instantiate(shield, transform.position + new Vector3(shieldHorizontal, 0, 0), Quaternion.identity);
        }
    }
}
