using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public Material mat;
    void Start()
    {
        this.gameObject.transform.Rotate(-90, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z);
        Debug.Log(GameManager.instance.wapon);
        if (GameManager.instance.wapon == "pistola")
            mat.color = Color.red;
        else if (GameManager.instance.wapon == "escopeta")
            mat.color = Color.yellow;
        else
            mat.color = Color.white;
    }
}
