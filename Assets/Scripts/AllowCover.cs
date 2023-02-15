using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowCover : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerTakeCover>() != null)
        {
            collision.gameObject.GetComponent<PlayerTakeCover>().isAllowTakeCover = true;
            collision.gameObject.GetComponent<PlayerTakeCover>().coverDefineNum = this.GetComponentInParent<BoxControll>().coverDefineNum;
            collision.gameObject.GetComponent<PlayerTakeCover>().cover = this.transform.parent.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerTakeCover>() != null)
        {
            collision.gameObject.GetComponent<PlayerTakeCover>().isAllowTakeCover = false;
            collision.gameObject.GetComponent<PlayerTakeCover>().coverDefineNum = 0;
        }
    }
}
