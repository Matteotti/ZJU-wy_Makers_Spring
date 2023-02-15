using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdiotEnemy : MonoBehaviour
{
    public GameObject blackIdiot;
    public GameObject whiteIdiot;
    private void Start()
    {
        blackIdiot = GameObject.Find("BlackIdiot");
        whiteIdiot = GameObject.Find("WhiteIdiot");
    }
    public void Idiot(int coverDefine)
    {
        if(coverDefine == 3)
        {
            blackIdiot.GetComponent<Animator>().SetBool("IsAttacking", true);
            whiteIdiot.GetComponent<Animator>().SetBool("IsAttacking", true);
        }
    }
}
