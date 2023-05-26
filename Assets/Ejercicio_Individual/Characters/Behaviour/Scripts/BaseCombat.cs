using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCombat : MonoBehaviour, IAttackReadable
{
    [SerializeField] Transform attackCollidersParent;
    [SerializeField] float attackDuration = 0.3f;
    protected bool mustAttack;

    void Start()
    {
        foreach (Transform t in attackCollidersParent) { t.gameObject.SetActive(false); }
    }

    bool IAttackReadable.CheckAttack()
    {
        bool attack = mustAttack;
        mustAttack = false;
        return attack;
    }

    internal void OnAnimationHit(string colliderName)
    {
        Transform colliderTransform = attackCollidersParent.Find(colliderName);
        if (colliderTransform)
        {
            colliderTransform.gameObject.SetActive(true);
            //DOVirtual.DelayedCall(attackDuration, () => colliderTransform.gameObject.SetActive(false));
        }
    }
}
