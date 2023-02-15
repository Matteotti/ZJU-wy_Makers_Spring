using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallel : MonoBehaviour
{
    public Transform player;
    public float parallelNum;
    private void Update()
    {
        if(this.transform.position != new Vector3(player.position.x * parallelNum, player.position.y * parallelNum, 0))
            this.transform.position = new Vector3 (player.position.x * parallelNum, player.position.y * parallelNum, 0);
    }
}
