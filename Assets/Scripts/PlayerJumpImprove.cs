using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpImprove : MonoBehaviour
{
    public float rayPosHorizontal;
    public float rayPosVertical;
    public float rayPosForHeadVertical;
    public float rayPosForHeadHorizontal;
    public float rayCheckDistanceFoot;
    public float rayCheckDistanceHead;
    public float playerUpDistance;
    public RaycastHit2D hitFoot;
    public RaycastHit2D hitHead;
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(this.transform.localScale.x == 1)
        {
            hitFoot = Physics2D.Raycast(this.transform.position + new Vector3(rayPosHorizontal, rayPosVertical, 0), Vector2.down, rayCheckDistanceFoot);
            Debug.DrawRay(this.transform.position + new Vector3(rayPosHorizontal, rayPosVertical, 0), Vector2.down * rayCheckDistanceFoot, Color.red);
            hitHead = Physics2D.Raycast(this.transform.position + new Vector3(rayPosForHeadHorizontal, rayPosForHeadVertical, 0), Vector2.down, rayCheckDistanceHead, 1 << 6 | 1 << 10 | 1 << 7);
            Debug.DrawRay(this.transform.position + new Vector3(rayPosForHeadHorizontal, rayPosForHeadVertical, 0), Vector2.down * rayCheckDistanceHead);
        }
        else if (this.transform.localScale.x == -1)
        {
            hitFoot = Physics2D.Raycast(this.transform.position + new Vector3(-rayPosHorizontal, rayPosVertical, 0), Vector2.down, rayCheckDistanceFoot);
            Debug.DrawRay(this.transform.position + new Vector3(-rayPosHorizontal, rayPosVertical, 0), Vector2.down * rayCheckDistanceFoot, Color.red);
            hitHead = Physics2D.Raycast(this.transform.position + new Vector3(-rayPosForHeadHorizontal, rayPosForHeadVertical, 0), Vector2.down, rayCheckDistanceHead, 1 << 6 | 1 << 10 | 1 << 7);
            Debug.DrawRay(this.transform.position + new Vector3(-rayPosForHeadHorizontal, rayPosForHeadVertical, 0), Vector2.down * rayCheckDistanceHead);
        }
        //Debug.Log("FOOT " + hitFoot.collider);
        //Debug.Log("HEAD " + hitHead.collider);
        if(hitFoot.collider != null && (hitFoot.collider.CompareTag("Wall") || hitFoot.collider.CompareTag("Ground") || hitFoot.collider.CompareTag("EnemyBody")) && hitHead.collider == null && Input.GetAxisRaw("Horizontal") == this.transform.localScale.x)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetBool("IsClimb", true);
        }
    }
    void ExitClimb()
    {
        animator.SetBool("IsClimb", false);
        animator.SetBool("FallToGround", true);
        //animator.SetBool("IsWalking", true);
        //animator.SetBool("IsRunning", true);
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.transform.position = hitFoot.point + new Vector2(0, playerUpDistance);
    }
}