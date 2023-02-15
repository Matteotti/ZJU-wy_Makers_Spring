using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public bool unLock;
    public GameObject Gear1;
    public GameObject Gear2;
    public GameObject Gear3;
    public GameObject Gear4;
    public GameObject key;
    public float speed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>().isHoldingKey)
            {
                collision.gameObject.GetComponent<PlayerController>().key.SendMessage("OpenDoor", this.gameObject, SendMessageOptions.DontRequireReceiver);
                key = collision.gameObject.GetComponent<PlayerController>().key;
            }
        }
    }
    private void Update()
    {
        if(unLock)
        {
            Invoke("Destroy", 2);
            Gear1.transform.rotation = Quaternion.Euler(Gear1.transform.rotation.eulerAngles + new Vector3(0, 0, speed));
            Gear2.transform.rotation = Quaternion.Euler(Gear2.transform.rotation.eulerAngles + new Vector3(0, 0, -speed));
            Gear3.transform.rotation = Quaternion.Euler(Gear3.transform.rotation.eulerAngles + new Vector3(0, 0, 2 * speed));
            Gear4.transform.rotation = Quaternion.Euler(Gear4.transform.rotation.eulerAngles + new Vector3(0, 0, -2 * speed));
        }
    }
    void Destroy()
    {
        Destroy(key);
        Destroy(this.gameObject);
    }
}
