using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Vector3 idleRotation = new Vector3(14.3659525f, 1.44314454e-05f, 5.2608666e-06f);
    public Vector3 idlePosition = new Vector3(0.569999993f, 0.300000012f, -0.280000001f);

    private Vector3 runRotation = new Vector3(-33.699f, 261.457886f, -79.392f);
    private Vector3 runPosition = new Vector3(0.0451871529f, -0.00439817924f, 0.0210952461f);

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
