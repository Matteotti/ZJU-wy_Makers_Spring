using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossHitFloor : MonoBehaviour
{
    public bool attack = true;
    public bool attackStart;
    public bool isCrackOver;
    public bool isBreakOver;
    public bool isShootOver;
    public int pos1;
    public int pos2;
    public int animIndex;
    public float counter;
    public float crackTime;
    public float breakTime;
    public float shootTime;
    public float recoverTime;
    public float posVertical;
    public float enemySpeed;
    public float playerSpeed;
    public float boxSpeed;
    public int[,] tilemapTile = new int[100, 100];
    public GameObject floorWave;
    public GameObject player;
    public GameObject createBox1;
    public GameObject createBox2;
    public GameObject blackBody;
    public GameObject whiteBody;
    public GameObject goldenBody;
    public GameObject crackTileLeftAnim;
    public GameObject crackTileRightAnim;
    public GameObject FlashWave;
    public Tilemap tilemap;
    public Tile crackTileLeft;
    public Tile crackTileRight;
    public Tile normalTileLeft;
    public Tile normalTileRight;
    public Vector3Int enemy1Pos;
    public Vector3Int enemy2Pos;
    public Vector3Int playerPos;
    //ÁÑºÛ£¬ÆÆËé£¬ÉÁ¹â£¬»Ö¸´
    private void Start()
    {

    }
    private void Update()
    {
        if (attack)
        {
            HitFloor();
            attack = false;
        }
        if (attackStart)
        {
            counter += Time.deltaTime;
            if (counter > crackTime && !isCrackOver)
            {
                Crack();
            }
            if (counter > breakTime && !isBreakOver)
            {
                Break();
            }
            if (counter > shootTime && !isShootOver)
            {
                Shoot();
            }
            if (counter > recoverTime)
            {
                Recover();
                attackStart = false;
                counter = 0;
                isCrackOver = false;
                isBreakOver = false;
                isShootOver = false;
            }
        }

    }

    void HitFloor()
    {
        if(this.GetComponent<BossControl>().isAngry)
            Instantiate(floorWave, this.transform.position - new Vector3(0, posVertical, 0), Quaternion.identity);
        attackStart = true;
        //ÈËÎïÍË³öÎ±×°
        if (player.GetComponent<PlayerController>().isTakingCover)
        {
            if (player.transform.localScale.x == 1)
            {
                switch (player.GetComponent<PlayerTakeCover>().coverBeTakenDefine)
                {
                    case 1:
                        createBox1 = Instantiate(blackBody, player.transform.position, Quaternion.identity);
                        if (this.GetComponent<BossCreateEnemy>().box1 == null)
                            this.GetComponent<BossCreateEnemy>().box1 = createBox1;
                        if (this.GetComponent<BossCreateEnemy>().box2 == null)
                            this.GetComponent<BossCreateEnemy>().box2 = createBox1;
                        if (this.GetComponent<BossCreateEnemy>().box3 == null)
                            this.GetComponent<BossCreateEnemy>().box3 = createBox1;
                        break;
                    case 2:
                        createBox1 = Instantiate(whiteBody, player.transform.position, Quaternion.identity);
                        if (this.GetComponent<BossCreateEnemy>().box1 == null)
                            this.GetComponent<BossCreateEnemy>().box1 = createBox1;
                        if (this.GetComponent<BossCreateEnemy>().box2 == null)
                            this.GetComponent<BossCreateEnemy>().box2 = createBox1;
                        if (this.GetComponent<BossCreateEnemy>().box3 == null)
                            this.GetComponent<BossCreateEnemy>().box3 = createBox1;
                        break;
                    case 3:
                        createBox1 = Instantiate(goldenBody, player.transform.position, Quaternion.identity);
                        if (this.GetComponent<BossCreateEnemy>().box1 == null)
                            this.GetComponent<BossCreateEnemy>().box1 = createBox1;
                        if (this.GetComponent<BossCreateEnemy>().box2 == null)
                            this.GetComponent<BossCreateEnemy>().box2 = createBox1;
                        if (this.GetComponent<BossCreateEnemy>().box3 == null)
                            this.GetComponent<BossCreateEnemy>().box3 = createBox1;
                        break;
                    default:
                        break;
                }
            }
            else if (player.transform.localScale.x == -1)
            {
                switch (player.GetComponent<PlayerTakeCover>().coverBeTakenDefine)
                {
                    case 1:
                        createBox2 = Instantiate(blackBody, player.transform.position, Quaternion.Euler(0, 0, 0));
                        createBox2.transform.localScale = new Vector3(-1, 1, 1);
                        if (this.GetComponent<BossCreateEnemy>().box1 == null)
                            this.GetComponent<BossCreateEnemy>().box1 = createBox2;
                        if (this.GetComponent<BossCreateEnemy>().box2 == null)
                            this.GetComponent<BossCreateEnemy>().box2 = createBox2;
                        if (this.GetComponent<BossCreateEnemy>().box3 == null)
                            this.GetComponent<BossCreateEnemy>().box3 = createBox2;
                        break;
                    case 2:
                        createBox2 = Instantiate(whiteBody, player.transform.position, Quaternion.Euler(0, 0, 0));
                        createBox2.transform.localScale = new Vector3(-1, 1, 1);
                        if (this.GetComponent<BossCreateEnemy>().box1 == null)
                            this.GetComponent<BossCreateEnemy>().box1 = createBox2;
                        if (this.GetComponent<BossCreateEnemy>().box2 == null)
                            this.GetComponent<BossCreateEnemy>().box2 = createBox2;
                        if (this.GetComponent<BossCreateEnemy>().box3 == null)
                            this.GetComponent<BossCreateEnemy>().box3 = createBox2;
                        break;
                    case 3:
                        createBox2 = Instantiate(goldenBody, player.transform.position, Quaternion.Euler(0, 0, 0));
                        createBox2.transform.localScale = new Vector3(-1, 1, 1);
                        if (this.GetComponent<BossCreateEnemy>().box1 == null)
                            this.GetComponent<BossCreateEnemy>().box1 = createBox2;
                        if (this.GetComponent<BossCreateEnemy>().box2 == null)
                            this.GetComponent<BossCreateEnemy>().box2 = createBox2;
                        if (this.GetComponent<BossCreateEnemy>().box3 == null)
                            this.GetComponent<BossCreateEnemy>().box3 = createBox2;
                        break;
                    default:
                        break;
                }
            }
            player.GetComponent<PlayerController>().jumpTime = 1;
            player.GetComponent<PlayerController>().isJumpButtonDown = false;
            Destroy(player.GetComponent<PlayerTakeCover>().nowSpring);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            player.GetComponent<PlayerTakeCover>().coverBeTakenDefine = 0;
            if (player.transform.localScale.x == 1)
                player.transform.position = new Vector3(player.transform.position.x - player.GetComponent<PlayerTakeCover>().deltaX, player.transform.position.y - player.GetComponent<PlayerTakeCover>().deltaY, player.transform.position.z);
            else if (player.transform.localScale.x == -1)
                player.transform.position = new Vector3(player.transform.position.x + player.GetComponent<PlayerTakeCover>().deltaX, player.transform.position.y - player.GetComponent<PlayerTakeCover>().deltaY, player.transform.position.z);
            player.GetComponent<PlayerController>().isTakingCover = false;
            player.GetComponent<CapsuleCollider2D>().enabled = true;
            player.transform.GetChild(2).gameObject.SetActive(false);
            player.transform.GetChild(0).gameObject.SetActive(true);
            player.GetComponent<Animator>().runtimeAnimatorController = player.transform.GetComponent<PlayerTakeCover>().playerAnim;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        //µÐÈË¾²Ö¹
        if (this.GetComponent<BossCreateEnemy>().enemy1 != null)
            this.GetComponent<BossCreateEnemy>().enemy1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        if (this.GetComponent<BossCreateEnemy>().enemy2 != null)
            this.GetComponent<BossCreateEnemy>().enemy2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
    void Crack()
    {
        if (this.GetComponent<BossCreateEnemy>().enemy1 != null)
        {
            enemy1Pos = this.GetComponent<BossCreateEnemy>().enemy1.GetComponent<GetBossEnemyTilePos>().tilePos;
            this.GetComponent<BossCreateEnemy>().enemy1.GetComponent<HostileEnemyMove>().isAllowShutDown = false;
        }
        else if (this.GetComponent<BossCreateEnemy>().box1 != null)
        {
            enemy1Pos = this.GetComponent<BossCreateEnemy>().box1.GetComponent<BoxControll>().BodyPos;
        }
        if (this.GetComponent<BossCreateEnemy>().enemy2 != null)
        {
            enemy2Pos = this.GetComponent<BossCreateEnemy>().enemy2.GetComponent<GetBossEnemyTilePos>().tilePos;
            this.GetComponent<BossCreateEnemy>().enemy2.GetComponent<HostileEnemyMove>().isAllowShutDown = false;
        }
        else if (this.GetComponent<BossCreateEnemy>().box2 != null)
        {
            enemy2Pos = this.GetComponent<BossCreateEnemy>().box2.GetComponent<BoxControll>().BodyPos;
        }
        if (player.transform.GetChild(0).gameObject.activeSelf)
            playerPos = player.transform.GetChild(0).GetComponent<GetPlayerTilePos>().tilePos;
        while(!tilemap.GetTile(playerPos))
        {
            //Debug.Log("GOT");
            playerPos -= new Vector3Int(0, 1, 0);
        }
        int i, j;
        for (i = -1; i <= 1; i++)
        {
            for (j = 0; j >= -3; j--)
            {
                if (tilemap.GetTile(enemy1Pos + new Vector3Int(i, j, 0)) == normalTileLeft)
                {
                    tilemapTile[enemy1Pos.x + i + 100, enemy1Pos.y + j + 100] = 0;
                    tilemap.SetTile(enemy1Pos + new Vector3Int(i, j, 0), crackTileLeft);
                }
                if (tilemap.GetTile(enemy1Pos + new Vector3Int(i, j, 0)) == normalTileRight)
                {
                    tilemapTile[enemy1Pos.x + i + 100, enemy1Pos.y + j + 100] = 1;
                    tilemap.SetTile(enemy1Pos + new Vector3Int(i, j, 0), crackTileRight);
                }
            }
        }
        for (i = -1; i <= 1; i++)
        {
            for (j = 0; j >= -3; j--)
            {
                if (tilemap.GetTile(enemy2Pos + new Vector3Int(i, j, 0)) == normalTileLeft)
                {
                    tilemapTile[enemy2Pos.x + i + 100, enemy2Pos.y + j + 100] = 0;
                    tilemap.SetTile(enemy2Pos + new Vector3Int(i, j, 0), crackTileLeft);
                }
                if (tilemap.GetTile(enemy2Pos + new Vector3Int(i, j, 0)) == normalTileRight)
                {
                    tilemapTile[enemy2Pos.x + i + 100, enemy2Pos.y + j + 100] = 1;
                    tilemap.SetTile(enemy2Pos + new Vector3Int(i, j, 0), crackTileRight);
                }
            }
        }
        pos1 = Random.Range(-5, 5);
        pos2 = Random.Range(-5, 5);
        for (i = 0; i <= 0; i++)
        {
            for (j = 0; j >= -1; j--)
            {
                if (tilemap.GetTile(playerPos + new Vector3Int(pos1 + i, j, 0)) == normalTileLeft)
                {
                    tilemapTile[playerPos.x + pos1 + i + 100, playerPos.y + j + 100] = 0;
                    tilemap.SetTile(playerPos + new Vector3Int(pos1 + i, j, 0), crackTileLeft);
                }
                if (tilemap.GetTile(playerPos + new Vector3Int(pos1 + i, j, 0)) == normalTileRight)
                {
                    tilemapTile[playerPos.x + pos1 + i + 100, playerPos.y + j + 100] = 1;
                    tilemap.SetTile(playerPos + new Vector3Int(pos1 + i, j, 0), crackTileRight);
                }
            }
        }
        for (i = 0; i <= 0; i++)
        {
            for (j = 0; j >= -1; j--)
            {
                if (tilemap.GetTile(playerPos + new Vector3Int(pos2 + i, j, 0)) == normalTileLeft)
                {
                    tilemapTile[playerPos.x + pos2 + i + 100, playerPos.y + j + 100] = 0;
                    tilemap.SetTile(playerPos + new Vector3Int(pos2 + i, j, 0), crackTileLeft);
                }
                if (tilemap.GetTile(playerPos + new Vector3Int(pos2 + i, j, 0)) == normalTileRight)
                {
                    tilemapTile[playerPos.x + pos2 + i + 100, playerPos.y + j + 100] = 1;
                    tilemap.SetTile(playerPos + new Vector3Int(pos2 + i, j, 0), crackTileRight);
                }
            }
        }
        isCrackOver = true;
    }
    void Break()
    {
        animIndex = 0;
        isBreakOver = true;
        int i, j;
        for (i = -1; i <= 1; i++)
        {
            for (j = 0; j >= -3; j--)
            {
                if (tilemap.GetTile(enemy1Pos + new Vector3Int(i, j, 0)) == crackTileLeft)
                {
                    Instantiate(crackTileLeftAnim, new Vector3(0.5f, 0.5f, 0.5f) + tilemap.CellToWorld(enemy1Pos + new Vector3Int(i, j, 0)), Quaternion.identity);
                }
                if (tilemap.GetTile(enemy1Pos + new Vector3Int(i, j, 0)) == crackTileRight)
                {
                    Instantiate(crackTileRightAnim, new Vector3(0.5f, 0.5f, 0.5f) + tilemap.CellToWorld(enemy1Pos + new Vector3Int(i, j, 0)), Quaternion.identity);
                }
            }
        }
        for (i = -1; i <= 1; i++)
        {
            for (j = 0; j >= -3; j--)
            {
                if (tilemap.GetTile(enemy2Pos + new Vector3Int(i, j, 0)) == crackTileLeft)
                {
                    Instantiate(crackTileLeftAnim, new Vector3(0.5f, 0.5f, 0.5f) + tilemap.CellToWorld(enemy2Pos + new Vector3Int(i, j, 0)), Quaternion.identity);
                }
                if (tilemap.GetTile(enemy2Pos + new Vector3Int(i, j, 0)) == crackTileRight)
                {
                    Instantiate(crackTileRightAnim, new Vector3(0.5f, 0.5f, 0.5f) + tilemap.CellToWorld(enemy2Pos + new Vector3Int(i, j, 0)), Quaternion.identity);
                }
            }
        }
        for (i = 0; i <= 0; i++)
        {
            for (j = 0; j >= -1; j--)
            {
                if (tilemap.GetTile(playerPos + new Vector3Int(pos1 + i, j, 0)) == crackTileLeft)
                {
                    Instantiate(crackTileLeftAnim, new Vector3(0.5f, 0.5f, 0.5f) + tilemap.CellToWorld(playerPos + new Vector3Int(pos1 + i, j, 0)), Quaternion.identity);
                }
                if (tilemap.GetTile(playerPos + new Vector3Int(pos1 + i, j, 0)) == crackTileRight)
                {
                    Instantiate(crackTileRightAnim, new Vector3(0.5f, 0.5f, 0.5f) + tilemap.CellToWorld(playerPos + new Vector3Int(pos1 + i, j, 0)), Quaternion.identity);
                }
            }
        }
        for (i = 0; i <= 0; i++)
        {
            for (j = 0; j >= -1; j--)
            {
                if (tilemap.GetTile(playerPos + new Vector3Int(pos2 + i, j, 0)) == crackTileLeft)
                {
                    Instantiate(crackTileLeftAnim, new Vector3(0.5f, 0.5f, 0.5f) + tilemap.CellToWorld(playerPos + new Vector3Int(pos2 + i, j, 0)), Quaternion.identity);
                }
                if (tilemap.GetTile(playerPos + new Vector3Int(pos2 + i, j, 0)) == crackTileRight)
                {
                    Instantiate(crackTileRightAnim, new Vector3(0.5f, 0.5f, 0.5f) + tilemap.CellToWorld(playerPos + new Vector3Int(pos2 + i, j, 0)), Quaternion.identity);
                }

            }
        }
        for (i = -1; i <= 1; i++)
        {
            for (j = 0; j >= -3; j--)
            {
                tilemap.SetTile(enemy1Pos + new Vector3Int(i, j, 0), null);
            }
        }
        for (i = -1; i <= 1; i++)
        {
            for (j = 0; j >= -3; j--)
            {
                tilemap.SetTile(enemy2Pos + new Vector3Int(i, j, 0), null);
            }
        }
        for (i = 0; i <= 0; i++)
        {
            for (j = 0; j >= -1; j--)
            {
                tilemap.SetTile(playerPos + new Vector3Int(pos1 + i, j, 0), null);
            }
        }
        for (i = 0; i <= 0; i++)
        {
            for (j = 0; j >= -1; j--)
            {
                tilemap.SetTile(playerPos + new Vector3Int(pos2 + i, j, 0), null);
            }
        }
    }
    void Recover()
    {
        if (this.GetComponent<BossCreateEnemy>().enemy1 != null)
        {
            this.GetComponent<BossCreateEnemy>().enemy1.GetComponent<Rigidbody2D>().velocity = enemySpeed * Vector2.up;
            this.GetComponent<BossCreateEnemy>().enemy1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (this.GetComponent<BossCreateEnemy>().box1 != null)
        {
            this.GetComponent<BossCreateEnemy>().box1.GetComponent<Rigidbody2D>().velocity = boxSpeed * Vector2.up;
            this.GetComponent<BossCreateEnemy>().box1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (this.GetComponent<BossCreateEnemy>().enemy2 != null)
        {
            this.GetComponent<BossCreateEnemy>().enemy2.GetComponent<Rigidbody2D>().velocity = enemySpeed * Vector2.up;
            this.GetComponent<BossCreateEnemy>().enemy2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (this.GetComponent<BossCreateEnemy>().box2 != null)
        {
            this.GetComponent<BossCreateEnemy>().box2.GetComponent<Rigidbody2D>().velocity = boxSpeed * Vector2.up;
            this.GetComponent<BossCreateEnemy>().box2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        player.GetComponent<Rigidbody2D>().velocity = Vector2.up * playerSpeed;
        Invoke("TileBack", 0.5f);
    }
    void Shoot()
    {
        Instantiate(FlashWave, transform.position, Quaternion.identity);
        isShootOver = true;
    }
    void TileBack()
    {
        int i, j;
        if (this.GetComponent<BossCreateEnemy>().enemy1 != null || this.GetComponent<BossCreateEnemy>().box1 != null)
            for (i = -1; i <= 1; i++)
            {
                for (j = 0; j >= -3; j--)
                {
                    if (tilemapTile[enemy1Pos.x + i + 100, enemy1Pos.y + j + 100] == 0)
                        tilemap.SetTile(enemy1Pos + new Vector3Int(i, j, 0), normalTileLeft);
                    if (tilemapTile[enemy1Pos.x + i + 100, enemy1Pos.y + j + 100] == 1)
                        tilemap.SetTile(enemy1Pos + new Vector3Int(i, j, 0), normalTileRight);
                }
            }
        if (this.GetComponent<BossCreateEnemy>().enemy2 != null || this.GetComponent<BossCreateEnemy>().box2 != null)
            for (i = -1; i <= 1; i++)
            {
                for (j = 0; j >= -3; j--)
                {
                    if (tilemapTile[enemy2Pos.x + i + 100, enemy2Pos.y + j + 100] == 0)
                        tilemap.SetTile(enemy2Pos + new Vector3Int(i, j, 0), normalTileLeft);
                    if (tilemapTile[enemy2Pos.x + i + 100, enemy2Pos.y + j + 100] == 1)
                        tilemap.SetTile(enemy2Pos + new Vector3Int(i, j, 0), normalTileRight);
                }
            }
        for (i = 0; i <= 0; i++)
        {
            for (j = 0; j >= -1; j--)
            {
                if (tilemapTile[playerPos.x + pos1 + i + 100, playerPos.y + j + 100] == 0)
                    tilemap.SetTile(playerPos + new Vector3Int(pos1 + i, j, 0), normalTileLeft);
                if (tilemapTile[playerPos.x + pos1 + i + 100, playerPos.y + j + 100] == 1)
                    tilemap.SetTile(playerPos + new Vector3Int(pos1 + i, j, 0), normalTileRight);
            }
        }
        for (i = 0; i <= 0; i++)
        {
            for (j = 0; j >= -1; j--)
            {
                if (tilemapTile[playerPos.x + pos2 + i + 100, playerPos.y + j + 100] == 0)
                    tilemap.SetTile(playerPos + new Vector3Int(pos2 + i, j, 0), normalTileLeft);
                if (tilemapTile[playerPos.x + pos2 + i + 100, playerPos.y + j + 100] == 1)
                    tilemap.SetTile(playerPos + new Vector3Int(pos2 + i, j, 0), normalTileRight);
            }
        }
        if(this.GetComponent<BossCreateEnemy>().enemy1 != null)
        {
            this.GetComponent<BossCreateEnemy>().enemy1.GetComponent<HostileEnemyMove>().isAllowShutDown = true;
        }
        if (this.GetComponent<BossCreateEnemy>().enemy2 != null)
        {
            this.GetComponent<BossCreateEnemy>().enemy2.GetComponent<HostileEnemyMove>().isAllowShutDown = true;
        }
    }
}
