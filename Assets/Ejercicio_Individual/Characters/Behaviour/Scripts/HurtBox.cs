using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HurtBox : MonoBehaviour
{
    [SerializeField] GameObject objectToDestroyOnHit;
    [SerializeField] public UnityEvent<HurtBox, HitBox> onHitNotified;

    public void NotifyHit(HitBox offender)
    {
        if (objectToDestroyOnHit) { Destroy(objectToDestroyOnHit); }
        onHitNotified.Invoke(this, offender);
    }
}
