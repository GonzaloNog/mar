using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsBase : MonoBehaviour
{
    [SerializeField] public Transform rightArmTarget;
    [SerializeField] public Transform rightArmHint;

    [SerializeField] public Transform leftArmTarget;
    [SerializeField] public Transform leftArmHint;



    public virtual void Shoot() { }
    public virtual void StartShooting() { }
    public virtual void stopShooting() { }

    public virtual bool AllowsOneShoot() { return false; }
    public virtual bool AllowsContinousShoot() { return false; }
}
