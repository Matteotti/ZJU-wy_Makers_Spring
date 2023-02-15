using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isPressed;
    public GameObject shield;
    private void Start()
    {
        shield = GameObject.Find("Shield(Clone)");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("EnemyBody"))
        {
            isPressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("EnemyBody"))
        {
            isPressed = false;
            if (!shield.activeSelf)
            {
                shield.SetActive(true);
                shield.GetComponent<Shield>().recover = true;
            }
        }
    }
}
