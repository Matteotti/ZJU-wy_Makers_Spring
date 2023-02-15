using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCameraControl : MonoBehaviour
{
    private void Disable()
    {
        Invoke("DisableImage", 10);
        Debug.Log("Disable");
    }
    void DisableImage()
    {
        this.gameObject.SetActive(false);
    }
}
