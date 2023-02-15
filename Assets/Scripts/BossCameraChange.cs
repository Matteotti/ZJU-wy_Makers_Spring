using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossCameraChange : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraPos;
    public GameObject mushroom;
    public GameObject King;
    public CinemachineVirtualCamera mainCamera;
    public bool boss;
    public float nowSize;
    public float initSize;
    public float speed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss = true;
            if(collision.GetComponent<PlayerController>() != null)
                collision.GetComponent<PlayerController>().jumpSpeed += 2;
            Invoke("Active", 3);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss = false;
            if (collision.GetComponent<PlayerController>() != null)
                collision.GetComponent<PlayerController>().jumpSpeed -= 2;
        }
    }
    private void Update()
    {
        if (boss)
        {
            player.GetComponent<PlayerController>().boss = true;
            mushroom.SetActive(false);
            mainCamera.Follow = cameraPos.transform;
            if (mainCamera.m_Lens.OrthographicSize < nowSize)
            {
                mainCamera.m_Lens.OrthographicSize += speed;
            }
            else
            {
                mainCamera.m_Lens.OrthographicSize = nowSize;
            }
            mainCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.5f;
            mainCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.5f;
        }
        else
        {
            player.GetComponent<PlayerController>().boss = false;
            mainCamera.Follow = player.transform;
            if (mainCamera.m_Lens.OrthographicSize > initSize)
            {
                mainCamera.m_Lens.OrthographicSize -= speed;
            }
            else
            {
                mainCamera.m_Lens.OrthographicSize = initSize;
            }
        }
    }
    void Active()
    {
        King.GetComponent<BossStateMachine>().enabled = true;
    }
}