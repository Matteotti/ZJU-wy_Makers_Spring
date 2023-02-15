using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowPlug : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.name == "Player")
        {
            collision.GetComponent<PlayerController>().isAllowingPlug = true;
            collision.SendMessage("Assign", this.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.name == "Player")
        {
            collision.GetComponent<PlayerController>().isAllowingPlug = false;
        }
    }
}
