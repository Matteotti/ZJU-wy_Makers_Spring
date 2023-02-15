using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BrokablePlatform : MonoBehaviour
{
    public GameObject player;
    public Tilemap tilemap;
    public Tile leftNormal;
    public Tile rightNormal;
    public GameObject leftBroken;
    public GameObject rightBroken;
    public bool isBroking = false;
    public bool isCollide = false;
    public float velocityYMin;
    public float playerVelocityY;
    private void Start()
    {
        tilemap = this.GetComponent<Tilemap>();
    }
    void Update()
    {
        
        if(isBroking)
        {
            Vector3Int currentPos = tilemap.WorldToCell(player.transform.position);
            currentPos.y--;
            if(tilemap.GetTile(currentPos) == leftNormal)
            {
                Instantiate(leftBroken, new Vector3(0.5f, 0.5f, 0) + tilemap.CellToWorld(currentPos), Quaternion.identity);
            }
            else if (tilemap.GetTile(currentPos) == rightNormal)
            {
                Instantiate(rightBroken, new Vector3(0.5f, 0.5f, 0) + tilemap.CellToWorld(currentPos), Quaternion.identity);
            }
                tilemap.SetTile(currentPos, null);
            //Debug.Log(currentPos);
        }
        if(!isCollide)
        {
            playerVelocityY = player.GetComponent<Rigidbody2D>().velocity.y;
            //Debug.Log(playerVelocityY);
            //可优化性能--------------------------------------------------------------------------------------------
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Mathf.Abs(playerVelocityY) > Mathf.Abs(velocityYMin))
        {
            isBroking = true;
        }
        isCollide = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBroking = false;
            isCollide = false;
        }
    }
}
