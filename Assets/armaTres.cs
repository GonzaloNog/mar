using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armaTres : MonoBehaviour
{
    public Vector3 idleRotation = new Vector3(350.340698f, 357.509613f, 91.6829071f);
    public Vector3 idlePosition = new Vector3(-0.0450000018f, -0.0109999999f, 0.00300000003f);

    private Vector3 runRotation = new Vector3(345.888458f, 345.755829f, 58.1232605f);
    private Vector3 runPosition = new Vector3(-0.0107293073f, -0.0308988243f, 0.0145469308f);

    public bool move = true;
    void Start()
    {
        transform.localRotation = Quaternion.Euler(idleRotation);
        transform.localPosition = idlePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.localRotation = Quaternion.Euler(runRotation);
            transform.localPosition = runPosition;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(idleRotation);
            transform.localPosition = idlePosition;
        }
    }
}
