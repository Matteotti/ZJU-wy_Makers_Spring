using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public GameObject gameObjectLeft;
    public GameObject gameObjectRight;
    public GameObject key;
    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;
    public float pullSpeed;
    public float boxCheckVertical;
    public float boxCheckHorizontal;
    public float boxCheckDistance;
    public float cameraChangeSpeed;
    public float lastFrameSpeedY;
    public float presentFrameSpeedY;
    public float standardSpeed;
    public bool isJumpButtonDown;
    public bool isPlugging;
    public bool isHurt = false;
    public bool isGrounded = true;
    public bool isAllowingPlug = false;
    public bool isPushingLeft = false;
    public bool isPushingRight = false;
    public bool isPullingLeft = false;
    public bool isPullingRight = false;
    public bool isHoldingKey = false;
    public bool isAllowCamera = false;
    public bool isTakingCover;
    public bool boss;
    public int jumpTime = 1;
    public int maxJumpTime = 2;
    public int HP;
    public Animator animator;
    public Rigidbody2D rb;
    public CinemachineVirtualCamera Cinemachine2D;
    private void Update()
    {
        presentFrameSpeedY = rb.velocity.y;
        if (this.GetComponent<SpriteRenderer>().sortingLayerName != "Mid")
        {
            this.GetComponent<SpriteRenderer>().sortingLayerName = "Mid";
        }
        if (Input.GetButtonDown("Jump") && !isHurt)
        {
            isJumpButtonDown = true;
        }
        if (isAllowingPlug && Input.GetKeyDown(KeyCode.Z))
        {
            isPlugging = true;
        }
        if (isPullingLeft || isPullingRight || isPushingLeft || isPushingRight)
        {
            moveSpeed = pullSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
        PullOrPush();
        if(!boss)
            ChangeCamera();
        if (!isTakingCover)
            SetPlayerAnimator();
        lastFrameSpeedY = presentFrameSpeedY;
    }
    void FixedUpdate()
    {
        PlayerMove();
        RefreshJumpTime();
    }

    void RefreshJumpTime()
    {
        if (isGrounded)
        {
            jumpTime = 1;
        }
    }

    void PlayerMove()
    {
        float playerMoveHorizontal = Input.GetAxisRaw("Horizontal");
        if (playerMoveHorizontal != 0 && !isHurt && !isPlugging && !isTakingCover)
        {
            this.transform.localScale = new Vector3(playerMoveHorizontal, this.transform.localScale.y, this.transform.localScale.z);
        }
        else if(playerMoveHorizontal != 0 && isTakingCover)
        {
            this.transform.localScale = new Vector3(-playerMoveHorizontal, this.transform.localScale.y, this.transform.localScale.z);
        }
        if (!isHurt && !isPlugging && !isTakingCover)
            rb.velocity = new Vector2(playerMoveHorizontal * moveSpeed, rb.velocity.y);
        if (playerMoveHorizontal > 0.01f)
            Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.3f;
        else if (playerMoveHorizontal < -0.01f)
            Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.7f;
        if(!isTakingCover)
        {
            DoubleJump();
            SetPlayerAnimator();
        }
    }

    void DoubleJump()
    {
        if (isJumpButtonDown && !Input.GetKey(KeyCode.Z))//¿É¸ü¸Ä
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
            else if (!isGrounded && jumpTime < maxJumpTime)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpTime++;
            }
            isJumpButtonDown = false;
        }
    }
    void PullOrPush()
    {
        RaycastHit2D right;
        RaycastHit2D left;
        float playerMoveHorizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.Z) && !isTakingCover)
        {
            right = Physics2D.Raycast(this.transform.position + new Vector3(boxCheckHorizontal, boxCheckVertical, 0), Vector2.right, boxCheckDistance, ~(1 << 8 | 1 << 2));
            left = Physics2D.Raycast(this.transform.position + new Vector3(boxCheckHorizontal, boxCheckVertical, 0), Vector2.left, boxCheckDistance, ~(1 << 8 | 1 << 2));
            Debug.DrawRay(this.transform.position + new Vector3(boxCheckHorizontal, boxCheckVertical, 0), Vector2.right * boxCheckDistance);
            Debug.DrawRay(this.transform.position + new Vector3(boxCheckHorizontal, boxCheckVertical, 0), Vector2.left * boxCheckDistance);
            if (left.collider != null && (left.collider.CompareTag("Wall") || left.collider.CompareTag("EnemyBody")))
            {
                if (playerMoveHorizontal > 0.1f)
                {
                    isPullingRight = true;
                    isPushingLeft = false;
                    isPushingRight = false;
                    isPullingLeft = false;
                    left.collider.GetComponent<BoxControll>().isFreeze = false;
                    gameObjectLeft = left.collider.gameObject;
                    left.collider.SendMessage("FollowRight", SendMessageOptions.DontRequireReceiver);
                }
                else if (playerMoveHorizontal < -0.1f)
                {
                    isPushingLeft = true;
                    isPullingRight = false;
                    isPushingRight = false;
                    isPullingLeft = false;
                    left.collider.GetComponent<BoxControll>().isFreeze = false;
                    gameObjectLeft = left.collider.gameObject;
                    left.collider.SendMessage("FollowLeft", SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    left.collider.GetComponent<BoxControll>().isFreeze = true;
                    isPullingRight = false;
                    isPushingLeft = false;
                    isPushingRight = false;
                    isPullingLeft = false;
                }
                if (gameObjectLeft != null && gameObjectLeft != left.collider.gameObject)
                    gameObjectLeft.GetComponent<BoxControll>().isFreeze = true;
//                else if (gameObjectLeft != null)
//                    gameObjectRight.GetComponent<BoxControll>().isFreeze = false;
            }
            else if (right.collider != null && (right.collider.CompareTag("Wall") || right.collider.CompareTag("EnemyBody")))
            {
                if (playerMoveHorizontal > 0.1f)
                {
                    isPushingRight = true;
                    isPullingRight = false;
                    isPushingLeft = false;
                    isPullingLeft = false;
                    right.collider.GetComponent<BoxControll>().isFreeze = false;
                    gameObjectRight = right.collider.gameObject;
                    right.collider.SendMessage("FollowRight", SendMessageOptions.DontRequireReceiver);
                }
                else if (playerMoveHorizontal < -0.1f)
                {
                    isPullingLeft = true;
                    isPullingRight = false;
                    isPushingLeft = false;
                    isPushingRight = false;
                    right.collider.GetComponent<BoxControll>().isFreeze = false;
                    gameObjectRight = right.collider.gameObject;
                    right.collider.SendMessage("FollowLeft", SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    right.collider.GetComponent<BoxControll>().isFreeze = true;
                    isPullingRight = false;
                    isPushingLeft = false;
                    isPushingRight = false;
                    isPullingLeft = false;
                }
                if (gameObjectRight != null && gameObjectRight != right.collider.gameObject)
                    gameObjectRight.GetComponent<BoxControll>().isFreeze = true;
//                else if (gameObjectLeft != null)
//                    gameObjectLeft.GetComponent<BoxControll>().isFreeze = false;
            }
            else
            {
                isPullingRight = false;
                isPushingLeft = false;
                isPushingRight = false;
                isPullingLeft = false;
            }
            //Debug.Log("LC " + left.collider);
            //Debug.Log("RC " + right.collider);
            //Debug.Log("L " + gameObjectLeft);
            //Debug.Log("R " + gameObjectRight);

        }
        else
        {
            isPullingRight = false;
            isPushingLeft = false;
            isPushingRight = false;
            isPullingLeft = false;
            if (gameObjectLeft != null)
                gameObjectLeft.GetComponent<BoxControll>().isFreeze = true;
            if (gameObjectRight != null)
                gameObjectRight.GetComponent<BoxControll>().isFreeze = true;
        }
    }
    void GetKey(GameObject poccesskey)
    {
        key = poccesskey;
        isHoldingKey = true;
    }
    void ChangeCamera()
    {
        var playerMoveVertical = Input.GetAxisRaw("Vertical");
        if (playerMoveVertical > 0.01f)
        {
            if(Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY < 0.87f)
            {
                Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY += cameraChangeSpeed;
            }
            else
                Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.87f;
        }
        else if (playerMoveVertical < -0.01f)
        {
            if (Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY > 0.18f)
            {
                Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY -= cameraChangeSpeed;
            }
            else
                Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.18f;
        }
        else
        {
            if (Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY > 0.64f)
            {
                Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY -= cameraChangeSpeed;
            }
            else if (Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY < 0.62f)
            {
                Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY += cameraChangeSpeed;
            }
            else
                Cinemachine2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.63f;
        }
    }
    void SetPlayerAnimator()
    {
        float playerMoveHorizontal = Input.GetAxisRaw("Horizontal");
        if (playerMoveHorizontal != 0 && !isHurt && !isPlugging && isGrounded)
        {
            if (moveSpeed == runSpeed)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", true);
            }
            else if (moveSpeed == walkSpeed)
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsRunning", false);
            }
        }
        else if (isHurt)
        {
            animator.SetBool("IsHurt", true);
            animator.SetBool("IsPlugging", false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            isPlugging = false;
        }
        else if (isPlugging)
        {
            animator.SetBool("IsPlugging", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
        }
        if (isPullingLeft || isPullingRight)
        {
            animator.SetBool("IsPulling", true);
        }
        else if (isPushingLeft || isPushingRight)
        {
            animator.SetBool("IsPushing", true);
        }
        else
        {
            animator.SetBool("IsPushing", false);
            animator.SetBool("IsPulling", false);
        }
        if(lastFrameSpeedY <= 1f && presentFrameSpeedY > 1f)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("StartJump", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("StartFall", false);
            animator.SetBool("Falling", false);
            animator.SetBool("FallToGround", false);
            animator.SetBool("IsAiring", true);
        }
        else if(lastFrameSpeedY > 1f && presentFrameSpeedY > 1f)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("Jumping", true);
            animator.SetBool("StartFall", false);
            animator.SetBool("Falling", false);
            animator.SetBool("FallToGround", false);
            animator.SetBool("StartJump", false);
        }
        else if(!isGrounded && lastFrameSpeedY > -1f && lastFrameSpeedY < 1f && presentFrameSpeedY > -1f && presentFrameSpeedY < 1f)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("StartFall", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("FallToGround", false);
            animator.SetBool("StartJump", false);
            animator.SetBool("IsAiring", true);
        }
        else if (lastFrameSpeedY < -1f && presentFrameSpeedY < -1f)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("Falling", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("StartFall", false);
            animator.SetBool("FallToGround", false);
            animator.SetBool("StartJump", false);
            animator.SetBool("IsAiring", true);
        }
        else if(lastFrameSpeedY < -1f && presentFrameSpeedY > -1f)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("FallToGround", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("StartFall", false);
            animator.SetBool("Falling", false);
            animator.SetBool("StartJump", false);
        }
        //Debug.Log("THIS " + presentFrameSpeedY + " LAST " + lastFrameSpeedY);
        //if(!isHurt)
        //{
        //    animator.SetBool("IsHurt", false);
        //}
    }
}