using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtHarmless : MonoBehaviour
{
    public bool allowHurt;
    public float hurtCounter;
    public float maxAllowTime;
    public float blinkCounter;
    public float transparentTime;
    public float opaqueTime;
    private void Update()
    {
        if (!allowHurt)
        {
            if (hurtCounter < maxAllowTime)
            {
                hurtCounter += Time.deltaTime;
                blinkCounter += Time.deltaTime;
                if (blinkCounter < transparentTime)
                {
                    this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    //透明
                }
                else if (blinkCounter > transparentTime && blinkCounter < transparentTime + opaqueTime)
                {
                    this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    //不透明
                }
                else
                {
                    blinkCounter = 0;
                }
            }
            else
            {
                hurtCounter = 0;
                blinkCounter = 0;
                allowHurt = true;
                this.GetComponent<PlayerController>().isHurt = false;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                //不透明
            }
        }
    }
}
