using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVelocityReadable
{
    Vector3 GetVelocity();

    float GetWalkSpeed();
    float GetRunSpeed();

    float GetJumpSpeed();

    bool IsGrounded();
}
