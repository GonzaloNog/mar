using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject target;
    public Vector3 position;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x + position.x, target.transform.position.y + position.y, target.transform.position.z + position.z);
    }
}
