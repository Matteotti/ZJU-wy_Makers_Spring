using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringTransparent : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
    private void Update()
    {
        if (GetComponent<SpriteRenderer>().color.a < 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a + speed);
        }
        else
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
