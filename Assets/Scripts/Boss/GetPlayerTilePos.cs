using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GetPlayerTilePos : MonoBehaviour
{
    public Vector3Int tilePos;
    public Tilemap tilemap;
    private void Start()
    {
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            tilePos = tilemap.WorldToCell(this.transform.position) - new Vector3Int(0, 1, 0);
        }
    }
}
