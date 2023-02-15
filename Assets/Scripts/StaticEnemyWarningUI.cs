using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticEnemyWarningUI : MonoBehaviour
{
    public GameObject warningBar;
    public GameObject thisWarningBar;
    public float UIPosY;
    private void Update()
    {
        if (this.GetComponent<HostileStaticEnemy>().isSpottedPlayer)
        {
            if (thisWarningBar == null)
            {
                thisWarningBar = Instantiate(warningBar, Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, UIPosY, 0)), Quaternion.identity, GameObject.Find("Canvas").transform);
                thisWarningBar.transform.SetAsFirstSibling();
            }
            if (this.GetComponent<HostileStaticEnemy>().attack && this.GetComponent<HostileStaticEnemy>().playerCheckCounter <= 0.1)
            {
                thisWarningBar.GetComponent<Slider>().value = 1;
                //Debug.Log("Spotted");
            }
            else if (this.GetComponent<HostileStaticEnemy>().playerCheckCounter > 0.1)
            {
                if (thisWarningBar.GetComponent<Slider>().value > 0)
                    thisWarningBar.GetComponent<Slider>().value -= 2 * Time.deltaTime / this.GetComponent<HostileStaticEnemy>().playerCheckLastTime;
                else
                    thisWarningBar.GetComponent<Slider>().value = 0;
                //Debug.Log("Forgetting");
            }
            else if (!this.GetComponent<HostileStaticEnemy>().attack && this.GetComponent<HostileStaticEnemy>().playerCheckCounter <= 0.1)
            {
                float value;
                if (this.GetComponent<HostileStaticEnemy>().playerMoveCounter == 0 && this.GetComponent<HostileStaticEnemy>().playerJumpCounter == 0)
                {
                    if (thisWarningBar.GetComponent<Slider>().value > 0)
                        value = thisWarningBar.GetComponent<Slider>().value - 3 * Time.deltaTime;
                    else
                        value = 0;
                }
                else if (this.GetComponent<HostileStaticEnemy>().playerMoveCounter / this.GetComponent<HostileStaticEnemy>().playerMoveMaxTimeAllowed > this.GetComponent<HostileStaticEnemy>().playerJumpCounter / this.GetComponent<HostileStaticEnemy>().playerJumpMaxTimeAllowed)
                    value = this.GetComponent<HostileStaticEnemy>().playerMoveCounter / this.GetComponent<HostileStaticEnemy>().playerMoveMaxTimeAllowed;
                else
                    value = this.GetComponent<HostileStaticEnemy>().playerJumpCounter / this.GetComponent<HostileStaticEnemy>().playerJumpMaxTimeAllowed;
                thisWarningBar.GetComponent<Slider>().value = value;
                //Debug.Log("Observing");
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
    private void OnDestroy()
    {
        if (thisWarningBar != null)
            Destroy(thisWarningBar);
    }
}