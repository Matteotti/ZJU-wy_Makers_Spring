using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject button;
    public bool recover;
    public float speed;
    private void Start()
    {
        button = GameObject.Find("Button(Clone)");
    }
    private void Update()
    {
        if(button.GetComponent<Button>().isPressed)
        {
            if(this.GetComponent<SpriteRenderer>().color.a > speed)
                this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, this.GetComponent<SpriteRenderer>().color.a - speed);
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, 0);
                this.gameObject.SetActive(false);
            }
        }
        else if(recover)
        {
            if (this.GetComponent<SpriteRenderer>().color.a < 1)
                this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, this.GetComponent<SpriteRenderer>().color.a + speed);
            else
                this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, 1);
        }
    }
}
