using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveEnemyWarningUI : MonoBehaviour
{
    public GameObject warningBar;
    public GameObject thisWarningBar;
    public float UIPosY;
    private void Update()
    {
        if(GetComponent<Animator>().GetBool("IsBroken"))
        {
            if (thisWarningBar != null)
                Destroy(thisWarningBar);
            return;
        }
        if (this.GetComponent<HostileEnemyMove>().isSpottedPlayer)
        {
            if (thisWarningBar == null && !GetComponent<Animator>().GetBool("IsBroken"))
            {
                thisWarningBar = Instantiate(warningBar, Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, UIPosY, 0)), Quaternion.identity, GameObject.Find("Canvas").transform);
                thisWarningBar.transform.SetAsFirstSibling();
            }
            if (this.GetComponent<HostileEnemyMove>().attack && this.GetComponent<HostileEnemyMove>().playerCheckCounter <= 0.1)
            {
                thisWarningBar.GetComponent<Slider>().value = 1;
            }
            else if (this.GetComponent<HostileEnemyMove>().playerCheckCounter > 0.1)
            {
                if (thisWarningBar.GetComponent<Slider>().value > 0)
                    thisWarningBar.GetComponent<Slider>().value -= 2 * Time.deltaTime / this.GetComponent<HostileEnemyMove>().playerCheckLastTime;
                else
                    thisWarningBar.GetComponent<Slider>().value = 0;
            }
            else if (!this.GetComponent<HostileEnemyMove>().attack && this.GetComponent<HostileEnemyMove>().playerCheckCounter <= 0.1)
            {
                float value;
                if (this.GetComponent<HostileEnemyMove>().playerMoveCounter == 0 && this.GetComponent<HostileEnemyMove>().playerJumpCounter == 0)
                {
                    if(thisWarningBar.GetComponent<Slider>().value > 0)
                        value = thisWarningBar.GetComponent<Slider>().value - 3 * Time.deltaTime;
                    else
                        value = 0;
                }
                else if (this.GetComponent<HostileEnemyMove>().playerMoveCounter / this.GetComponent<HostileEnemyMove>().playerMoveMaxTimeAllowed > this.GetComponent<HostileEnemyMove>().playerJumpCounter / this.GetComponent<HostileEnemyMove>().playerJumpMaxTimeAllowed)
                    value = this.GetComponent<HostileEnemyMove>().playerMoveCounter / this.GetComponent<HostileEnemyMove>().playerMoveMaxTimeAllowed;
                else
                    value = this.GetComponent<HostileEnemyMove>().playerJumpCounter / this.GetComponent<HostileEnemyMove>().playerJumpMaxTimeAllowed;
                thisWarningBar.GetComponent<Slider>().value = value;
            }

        }
        else
        {
            if (thisWarningBar != null)
            {
                Destroy(thisWarningBar);
                thisWarningBar = null;
            }
        }
        if (thisWarningBar != null)
        {
            thisWarningBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, UIPosY, 0));
        }
    }
}
