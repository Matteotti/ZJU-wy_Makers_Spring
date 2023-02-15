using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRebornEnemy : MonoBehaviour
{
    public bool reborn;
    public float buttonHorizontal;
    public float buttonVertical;
    public float shieldHorizontal;
    public GameObject button;
    public GameObject shield;
    public GameObject whiteEnemy;
    public GameObject blackEnemy;
    public GameObject goldenEnemy;
    private void Update()
    {
        if(reborn)
        {
            RebornEnemy();
        }
    }
    void RebornEnemy()
    {
        GameObject createBox;
        if (this.GetComponent<BossCreateEnemy>().enemy1 == null && this.GetComponent<BossCreateEnemy>().box1 != null)
        {
            Vector3 pos = this.GetComponent<BossCreateEnemy>().box1.transform.position;
            float scaleX = this.GetComponent<BossCreateEnemy>().box1.transform.localScale.x;
            int boxCoverDefine = this.GetComponent<BossCreateEnemy>().box1.GetComponent<BoxControll>().coverDefineNum;
            Destroy(this.GetComponent<BossCreateEnemy>().box1);
            if (scaleX == -1)
                switch (boxCoverDefine)
                {
                    case 1:
                        createBox = Instantiate(blackEnemy, pos, Quaternion.identity);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy1 = createBox;
                        this.GetComponent<BossCreateEnemy>().box1 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 2:
                        createBox = Instantiate(whiteEnemy, pos, Quaternion.identity);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy1 = createBox;
                        this.GetComponent<BossCreateEnemy>().box1 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 3:
                        createBox = Instantiate(goldenEnemy, pos, Quaternion.identity);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy1 = createBox;
                        this.GetComponent<BossCreateEnemy>().box1 = createBox.transform.GetChild(1).gameObject;
                        break;
                    default:
                        break;
                }
            else if (scaleX == 1)
                switch (boxCoverDefine)
                {
                    case 1:
                        createBox = Instantiate(blackEnemy, pos, Quaternion.identity);
                        createBox.transform.localScale = new Vector3(-1, 1, 1);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy1 = createBox;
                        this.GetComponent<BossCreateEnemy>().box1 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 2:
                        createBox = Instantiate(whiteEnemy, pos, Quaternion.identity);
                        createBox.transform.localScale = new Vector3(-1, 1, 1);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy1 = createBox;
                        this.GetComponent<BossCreateEnemy>().box1 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 3:
                        createBox = Instantiate(goldenEnemy, pos, Quaternion.identity);
                        createBox.transform.localScale = new Vector3(-1, 1, 1);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy1 = createBox;
                        this.GetComponent<BossCreateEnemy>().box1 = createBox.transform.GetChild(1).gameObject;
                        break;
                    default:
                        break;
                }
        }
        if (this.GetComponent<BossCreateEnemy>().enemy2 == null && this.GetComponent<BossCreateEnemy>().box2 != null)
        {
            Vector3 pos = this.GetComponent<BossCreateEnemy>().box2.transform.position;
            float scaleX = this.GetComponent<BossCreateEnemy>().box2.transform.localScale.x;
            int boxCoverDefine = this.GetComponent<BossCreateEnemy>().box2.GetComponent<BoxControll>().coverDefineNum;
            Destroy(this.GetComponent<BossCreateEnemy>().box2);
            if (scaleX == -1)
                switch (boxCoverDefine)
                {
                    case 1:
                        createBox = Instantiate(blackEnemy, pos, Quaternion.identity);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy2 = createBox;
                        this.GetComponent<BossCreateEnemy>().box2 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 2:
                        createBox = Instantiate(whiteEnemy, pos, Quaternion.identity);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy2 = createBox;
                        this.GetComponent<BossCreateEnemy>().box2 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 3:
                        createBox = Instantiate(goldenEnemy, pos, Quaternion.identity);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy2 = createBox;
                        this.GetComponent<BossCreateEnemy>().box2 = createBox.transform.GetChild(1).gameObject;
                        break;
                    default:
                        break;
                }
            else if (scaleX == 1)
                switch (boxCoverDefine)
                {
                    case 1:
                        createBox = Instantiate(blackEnemy, pos, Quaternion.identity);
                        createBox.transform.localScale = new Vector3(-1, 1, 1);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy2 = createBox;
                        this.GetComponent<BossCreateEnemy>().box2 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 2:
                        createBox = Instantiate(whiteEnemy, pos, Quaternion.identity);
                        createBox.transform.localScale = new Vector3(-1, 1, 1);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy2 = createBox;
                        this.GetComponent<BossCreateEnemy>().box2 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 3:
                        createBox = Instantiate(goldenEnemy, pos, Quaternion.identity);
                        createBox.transform.localScale = new Vector3(-1, 1, 1);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy2 = createBox;
                        this.GetComponent<BossCreateEnemy>().box2 = createBox.transform.GetChild(1).gameObject;
                        break;
                    default:
                        break;
                }
        }
        if (this.GetComponent<BossCreateEnemy>().enemy3 == null && this.GetComponent<BossCreateEnemy>().box3 != null)
        {
            Vector3 pos = this.GetComponent<BossCreateEnemy>().box3.transform.position;
            float scaleX = this.GetComponent<BossCreateEnemy>().box3.transform.localScale.x;
            int boxCoverDefine = this.GetComponent<BossCreateEnemy>().box3.GetComponent<BoxControll>().coverDefineNum;
            Destroy(this.GetComponent<BossCreateEnemy>().box3);
            if (scaleX == -1)
                switch (boxCoverDefine)
                {
                    case 1:
                        createBox = Instantiate(blackEnemy, pos, Quaternion.identity);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy3 = createBox;
                        this.GetComponent<BossCreateEnemy>().box3 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 2:
                        createBox = Instantiate(whiteEnemy, pos, Quaternion.identity);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy3 = createBox;
                        this.GetComponent<BossCreateEnemy>().box3 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 3:
                        createBox = Instantiate(goldenEnemy, pos, Quaternion.identity);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy3 = createBox;
                        this.GetComponent<BossCreateEnemy>().box3 = createBox.transform.GetChild(1).gameObject;
                        break;
                    default:
                        break;
                }
            else if (scaleX == 1)
                switch (boxCoverDefine)
                {
                    case 1:
                        createBox = Instantiate(blackEnemy, pos, Quaternion.identity);
                        createBox.transform.localScale = new Vector3(-1, 1, 1);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy3 = createBox;
                        this.GetComponent<BossCreateEnemy>().box3 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 2:
                        createBox = Instantiate(whiteEnemy, pos, Quaternion.identity);
                        createBox.transform.localScale = new Vector3(-1, 1, 1);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy3 = createBox;
                        this.GetComponent<BossCreateEnemy>().box3 = createBox.transform.GetChild(1).gameObject;
                        break;
                    case 3:
                        createBox = Instantiate(goldenEnemy, pos, Quaternion.identity);
                        createBox.transform.localScale = new Vector3(-1, 1, 1);
                        createBox.GetComponent<HostileEnemyMove>().isBossEnemy = true;
                        this.GetComponent<BossCreateEnemy>().enemy3 = createBox;
                        this.GetComponent<BossCreateEnemy>().box3 = createBox.transform.GetChild(1).gameObject;
                        break;
                    default:
                        break;
                }
        }
        if (this.GetComponent<BossControl>().isAngry)
        {
            if (GameObject.Find("Button(Clone)") == null)
                Instantiate(button, transform.position + new Vector3(buttonHorizontal, buttonVertical, 0), Quaternion.identity);
            if (GameObject.Find("Shield(Clone)") == null)
                Instantiate(shield, transform.position + new Vector3(shieldHorizontal, 0, 0), Quaternion.identity);
        }
        reborn = false;
    }
}
