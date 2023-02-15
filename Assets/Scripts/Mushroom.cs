using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Mushroom : MonoBehaviour
{
    public bool haveBeenGrounded;
    public float bounceNum;
    public float staticBounceNum;
    public float maxSpeed;
    public float maxCamera;
    public float initSize;
    public float recoverSpeed;
    public float changeSpeed;
    public float errorDebug;
    public float maxRange;
    public float power;
    public CinemachineVirtualCamera mainCamera;
    public GameObject player;
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player") && !player.GetComponent<PlayerController>().isTakingCover)
        {
            if (collision.GetComponent<PlayerController>().isGrounded == false)
            {
                animator.SetBool("BeJumpedOn",true);
                if (haveBeenGrounded)
                {
                    haveBeenGrounded = false;
                    collision.GetComponent<Rigidbody2D>().velocity =
                        bounceNum * ((collision.GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed) ? maxSpeed : collision.GetComponent<Rigidbody2D>().velocity.magnitude)
                        * Vector2.up;
                }
                else
                {
                    collision.GetComponent<Rigidbody2D>().velocity =
                        staticBounceNum * ((collision.GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed) ? maxSpeed : collision.GetComponent<Rigidbody2D>().velocity.magnitude)
                        * Vector2.up;
                }
                collision.GetComponent<PlayerController>().jumpTime = 2;
                collision.GetComponent<PlayerController>().isAllowCamera = true;
            }
        }
    }
    private void Update()
    {
        if (player.GetComponent<PlayerController>().isGrounded)
        {
            haveBeenGrounded = true;
            player.GetComponent<PlayerController>().isAllowCamera = false;
            maxRange = 0;
        }
        if (player.GetComponent<PlayerController>().isAllowCamera)
        {
            maxRange = (maxRange > (player.transform.position.y - this.transform.position.y))? maxRange : (player.transform.position.y - this.transform.position.y);
            if (maxRange > maxCamera)
                maxRange = maxCamera;
            if(mainCamera.m_Lens.OrthographicSize < maxRange * power)
            {
                mainCamera.m_Lens.OrthographicSize += changeSpeed;
            }
        }
        else
        {
            if (mainCamera.m_Lens.OrthographicSize > initSize)
            {
                mainCamera.m_Lens.OrthographicSize -= recoverSpeed;
            }
            else if (mainCamera.m_Lens.OrthographicSize != initSize)
            {
                mainCamera.m_Lens.OrthographicSize = initSize;
            }
        }
    }
    void EndPlay()
    {
        animator.SetBool("BeJumpedOn", false);
    }
}
