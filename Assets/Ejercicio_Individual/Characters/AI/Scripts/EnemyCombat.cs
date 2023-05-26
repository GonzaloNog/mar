using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCombat : BaseCombat
{
    bool combatEngaged;

    internal void StartCombat()
    {
        combatEngaged = true;
    }

    internal void StopCombat()
    {
        combatEngaged = false;
    }

    private void Update()
    {
        if (combatEngaged)
        {
            mustAttack = true;
        }
    }

    void OnAttack()
    {
        mustAttack = true;
    }
}
