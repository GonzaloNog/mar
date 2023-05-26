using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamageWithLife : CharacterDamage
{
    [SerializeField] float maxLife = 1f;
    float currentLife = 0f;

    private void Start()
    {
        currentLife = maxLife;
    }

    protected override void ProcessHit(HurtBox hurtBox, HitBox hitBox)
    {
        if (currentLife >= 0f)
        {
            currentLife -= 1f;
            if (currentLife <= 0f)
            {
                currentLife = 0f;
                Die();
            }
        }
    }

    public float GetMaxLife() { return maxLife; }
    public float GetCurrentLife() { return currentLife; }
    public float GetLifePercentage() { return currentLife / maxLife; }
}
