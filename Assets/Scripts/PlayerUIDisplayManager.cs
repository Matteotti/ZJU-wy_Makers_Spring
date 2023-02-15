using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIDisplayManager : MonoBehaviour
{
    public GameObject interactUI;
    public GameObject thisInteractUI;
    public GameObject canvas;
    public float verticalPos;
    public bool displayBox;
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
    private void Update()
    {
        if (this.GetComponent<PlayerController>().isAllowingPlug || (displayBox && !this.GetComponent<PlayerTakeCover>().isAllowTakeCover))
        {
            if (thisInteractUI == null)
            {
                thisInteractUI = Instantiate(interactUI, Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, verticalPos, 0)), Quaternion.identity, canvas.transform);
            }
            else
            {
                thisInteractUI.transform.position = this.transform.position + new Vector3(0, verticalPos, 0);
            }
        }
        else
        {
            if (thisInteractUI != null)
                thisInteractUI.GetComponent<InteractUIController>().isTransparent = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.CompareTag("Wall") || collision.collider.CompareTag("EnemyBody")) && this.transform.position.y <= collision.transform.position.y)
        {
            displayBox = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("EnemyBody"))
        {
            displayBox = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Porter"))
        {
            displayBox = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Porter"))
        {
            displayBox = false;
        }
    }
}
