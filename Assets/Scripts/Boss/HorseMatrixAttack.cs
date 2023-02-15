using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMatrixAttack : MonoBehaviour
{
    public bool attack;

    public GameObject horse;

    void Attack()
    {
        Instantiate(horse, this.transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if (attack)
        {
            Attack();
            attack = false;
        }
    }
}
