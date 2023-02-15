using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReborn : MonoBehaviour
{
    public Material blur;
    public Vector3 rebornPos;
    public RuntimeAnimatorController playerController;
    public bool isReborn;
    public float playerSpeedY;
    public float maxAllowSpeed;
    public float positionX;
    public float blurSpeed;
    public float blackSpeed;
    public bool finished_1;
    public bool finished_2;
    private void Start()
    {
        blur.SetFloat("_Size", 0);
        blur.SetColor("_Color", new Color(1, 1, 1, 1));
        blackSpeed = blurSpeed / 50;
    }
    private void Update()
    {
        if (!isReborn)
        {
            if (this.GetComponent<GroundCheck>().isGrounded || (this.GetComponentInChildren<PlayerCoverControl>() != null && this.GetComponentInChildren<PlayerCoverControl>().isGrounded))
            {
                if (playerSpeedY < -maxAllowSpeed)
                {
                    playerSpeedY = 0;
                    if (!isReborn)
                        isReborn = true;
                }
                if (!isReborn && this.GetComponent<Rigidbody2D>().velocity.x > 0.01f)
                {
                    rebornPos = transform.position - new Vector3(positionX, 0.5f, 0);
                    //Debug.Log("Remebering");
                }
                else if (!isReborn && this.GetComponent<Rigidbody2D>().velocity.x < -0.01f)
                {
                    rebornPos = transform.position + new Vector3(positionX, 0.5f, 0);
                    //Debug.Log("Remebering");
                }
            }
            else
            {
                playerSpeedY = (playerSpeedY < this.GetComponent<Rigidbody2D>().velocity.y) ? playerSpeedY : this.GetComponent<Rigidbody2D>().velocity.y;
                if (this.GetComponent<Rigidbody2D>().velocity.y > 0.1f)
                    playerSpeedY = 0;
            }
        }
        else
        {
            if (!finished_1 && blur.GetFloat("_Size") < 50)
            {
                blur.SetFloat("_Size", blur.GetFloat("_Size") + blurSpeed);
            }
            else if (!finished_1)
            {
                finished_1 = true;
                blur.SetFloat("_Size", 50);
            }
            if (!finished_1 && blur.GetColor("_Color").g > 0)
            {
                blur.SetColor("_Color", new Color(blur.GetColor("_Color").r - blackSpeed, blur.GetColor("_Color").g - blackSpeed, blur.GetColor("_Color").b - blackSpeed, blur.GetColor("_Color").a));
            }
            if (blur.GetFloat("_Size") == 50)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.transform.position = rebornPos;
                this.GetComponent<PlayerController>().HP--;
                this.GetComponent<PlayerHurtHarmless>().allowHurt = false;
            }
            if (finished_1 && blur.GetFloat("_Size") > 0)
            {
                blur.SetFloat("_Size", blur.GetFloat("_Size") - blurSpeed);
            }
            else if (finished_1 && blur.GetFloat("_Size") <= 0)
            {
                blur.SetFloat("_Size", 0);
                finished_2 = true;
            }
            if (finished_1 && blur.GetColor("_Color").g < 1)
            {
                blur.SetColor("_Color", new Color(blur.GetColor("_Color").r + blackSpeed, blur.GetColor("_Color").g + blackSpeed, blur.GetColor("_Color").b + blackSpeed, blur.GetColor("_Color").a));
            }
            else if (finished_1)
            {
                blur.SetColor("_Color", new Color(1, 1, 1, 1));
                this.GetComponent<PlayerHurtHarmless>().allowHurt = false;
            }
            if (finished_2)
            {
                finished_1 = false;
                finished_2 = false;
                isReborn = false;
                playerSpeedY = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            if (!isReborn)
                isReborn = true;
            if (this.GetComponent<PlayerController>().isTakingCover)
            {
                this.GetComponent<PlayerTakeCover>().QuitCover();
            }
        }
    }
}
