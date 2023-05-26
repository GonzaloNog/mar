using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCombatMessageForwarder : MonoBehaviour
{
    BaseCombat baseCombat;

    private void Awake()
    {
        baseCombat = GetComponentInParent<BaseCombat>();
    }

    void OnAnimationHit(string colliderName)
    {
        baseCombat?.OnAnimationHit(colliderName);
    }
}
