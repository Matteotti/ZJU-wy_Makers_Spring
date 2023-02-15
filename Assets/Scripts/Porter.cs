using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porter : MonoBehaviour
{
    public bool next;
    public bool next2;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerSceneChange>().isAllow = true;
            collision.GetComponent<PlayerSceneChange>().next = next;
            collision.GetComponent<PlayerSceneChange>().next2 = next2;
        }
    }
}
