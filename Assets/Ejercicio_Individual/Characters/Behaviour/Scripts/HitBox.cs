using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    [SerializeField] string[] targetTags;
    [SerializeField] public UnityEvent<HurtBox, HitBox> onHitNotified;
    [SerializeField] bool hitsOnTrigger = true;
    [SerializeField] bool hitsOnCollision = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (hitsOnCollision) { CheckHit(collision.collider); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitsOnTrigger) { CheckHit(other); }
    }

    void CheckHit(Collider other)
    {
        for (int i = 0; i < targetTags.Length; i++)
        {
            if (other.CompareTag(targetTags[i]))
            {
                HurtBox hurtBox = other.GetComponent<HurtBox>();
                if (hurtBox)
                {
                    hurtBox.NotifyHit(this);
                    onHitNotified.Invoke(hurtBox, this);
                }
            }
        }
    }
}
