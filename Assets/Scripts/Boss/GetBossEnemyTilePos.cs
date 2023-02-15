using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GetBossEnemyTilePos : MonoBehaviour
{
    public Vector3Int tilePos;
    public Tilemap tilemap;
    private void Start()
    {
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.GetComponent<HostileEnemyMove>().isBossEnemy && collision.collider.CompareTag("Ground"))
        {
            tilePos = tilemap.WorldToCell(this.transform.position) - new Vector3Int (0, 3, 0);
        }
    }
}
