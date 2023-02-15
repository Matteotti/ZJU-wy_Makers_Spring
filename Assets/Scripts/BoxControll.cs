using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxControll : MonoBehaviour
{
    public float boxCheckVertical;
    public float boxCheckHorizontal;
    public float boxCheckDistance;
    public GameObject player;
    public Tilemap tilemap;
    public bool isFreeze = true;
    public bool isBossEnemyBody;
    public bool colliderCheckRight = true;
    public bool colliderCheckLeft = true;
    public int coverDefineNum;// 1 - red 2 - blue 3 - yellow
    public Vector3Int BodyPos;
    public RaycastHit2D right;
    public RaycastHit2D left;
    private void Start()
    {
        player = GameObject.Find("Player");
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }
    void Update()
    {
        if (isFreeze)
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        right = Physics2D.Raycast(this.transform.position + new Vector3(boxCheckHorizontal, boxCheckVertical, 0), Vector2.right, boxCheckDistance, ~(1 << 8 | 1 << this.gameObject.layer | 1 << 2));
        left = Physics2D.Raycast(this.transform.position + new Vector3(-boxCheckHorizontal, boxCheckVertical, 0), Vector2.left, boxCheckDistance, ~(1 << 8 | 1 << this.gameObject.layer | 1 << 2));
        //Debug.DrawRay(this.transform.position + new Vector3(boxCheckHorizontal, boxCheckVertical, 0), Vector2.right * boxCheckDistance);
        //Debug.DrawRay(this.transform.position + new Vector3(-boxCheckHorizontal, boxCheckVertical, 0), Vector2.left * boxCheckDistance);
        //Debug.Log("L1:" + left.collider);
        //Debug.Log("R1:" + right.collider);
        if (left.collider != null)
        {
            colliderCheckLeft = false;
        }
        else
        {
            colliderCheckLeft = true;
        }
        if (right.collider != null)
        {
            colliderCheckRight = false;
        }
        else
        {
            colliderCheckRight = true;
        }
        right = Physics2D.Raycast(this.transform.position + new Vector3(boxCheckHorizontal, boxCheckVertical, 0), Vector2.right, boxCheckDistance, 1 << 8);
        left = Physics2D.Raycast(this.transform.position + new Vector3(boxCheckHorizontal, boxCheckVertical, 0), Vector2.left, boxCheckDistance, 1 << 8);
        //Debug.Log("L1:" + left.collider);
        //Debug.Log("R1:" + right.collider);
    }
    void FollowLeft()
    {
        if (colliderCheckLeft)
            this.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity;
    }
    void FollowRight()
    {
        if (colliderCheckRight)
            this.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity;
    }
    
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            BodyPos = tilemap.WorldToCell(this.transform.position) - new Vector3Int(0, 3, 0);
        }
    }
}
