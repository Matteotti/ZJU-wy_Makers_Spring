using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestory : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void DestroyWithBody()
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        if (this.GetComponent<HostileEnemyMove>().isBossEnemy)
        {
            this.transform.GetChild(1).GetComponent<BoxControll>().isBossEnemyBody = true;
        }
        Destroy(transform.GetChild(0).gameObject);
        this.transform.DetachChildren();
        Destroy(gameObject);
    }
}
