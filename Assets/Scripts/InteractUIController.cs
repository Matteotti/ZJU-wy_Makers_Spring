using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUIController : MonoBehaviour
{
    public bool isTransparent;
    public float speed;
    private void Start()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
    void Update()
    {
        if(isTransparent)
        {
            if(this.GetComponent<SpriteRenderer>().color.a > speed)
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, this.GetComponent<SpriteRenderer>().color.a - speed);
            else
                Destroy(this.gameObject);
        }
        else
        {
            if (this.GetComponent<SpriteRenderer>().color.a < 1)
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, this.GetComponent<SpriteRenderer>().color.a + speed);
            else
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
