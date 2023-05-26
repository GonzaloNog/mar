using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : BaseCombat
{
    void OnAttack()
    {
        mustAttack = true;
    }
}
