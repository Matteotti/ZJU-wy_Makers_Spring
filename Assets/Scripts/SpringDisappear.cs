using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringDisappear : MonoBehaviour
{
    public float speed = 0.03f;
    private void Start()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    void Update()
    {
        if(this.GetComponent<SpriteRenderer>().color.a > 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, this.GetComponent<SpriteRenderer>().color.a - speed);
        }
        else
        {
            Destroy(this.transform.parent.gameObject); 
        }
    }
}
