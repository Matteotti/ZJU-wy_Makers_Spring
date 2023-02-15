using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool timeBegin;
    public bool right;
    public float counter;
    public float bounceSpeedX;
    public float bounceSpeedY;
    public float bounceTime;
    public GameObject Player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerHurtHarmless>().allowHurt)
        {
            collision.gameObject.GetComponent<Animator>().SetBool("IsHurt", true);
            Player.GetComponent<Animator>().SetBool("IsPlugging", false);
            collision.gameObject.GetComponent<PlayerHurtHarmless>().allowHurt = false;
            timeBegin = true;
            if(this.transform.position.x < collision.transform.position.x)
            {
                right = true;
            }
            else
            {
                right = false;
            }
            collision.gameObject.GetComponent<PlayerController>().isHurt = true;
            //弹开？如何弹开？速度？时间？方向？
        }
    }
    private void Update()
    {
        if(timeBegin)
        {
            if (counter < bounceTime)
            {
                Player.GetComponent<Rigidbody2D>().velocity = new Vector2 ((right? 1 : -1) * bounceSpeedX, bounceSpeedY);
                counter += Time.deltaTime;
            }
            else
            {
                Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                counter = 0;
                timeBegin = false;
            }
        }
    }
}
