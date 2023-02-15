using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyAnimController : MonoBehaviour
{
    public Animator animator;
    public float counter;
    public float idleGap;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(counter <= idleGap && !animator.GetBool("IsIdle"))
        {
            counter += Time.deltaTime;
        }
        else
        {
            counter = 0;
            animator.SetBool("IsIdle", true);
        }
    }
    void StopIdle()
    {
        animator.SetBool("IsIdle", false);
    }
}

