using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isFollowing = false;
    public bool isOpening = false;
    public float followNum;
    public float posMax;
    public float speed;
    public GameObject keyPos;
    public GameObject door;
    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles + new Vector3(0, 0, speed));
        if (isFollowing)
        {
            this.transform.position += (keyPos.transform.position - this.transform.position) * followNum;
        }
        if (isOpening)
        {
            this.transform.position += (door.transform.position - this.transform.position) * followNum;
            if ((door.transform.position - this.transform.position).magnitude <= posMax)
            {
                
                door.GetComponent<LockedDoor>().unLock = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpening)
        {
            collision.SendMessage("GetKey", this.gameObject, SendMessageOptions.DontRequireReceiver);
            isFollowing = true;
        }
    }
    void OpenDoor(GameObject proccessDoor)
    {
        isFollowing = false;
        isOpening = true;
        door = proccessDoor;
    }
}
