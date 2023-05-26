using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterDamage : MonoBehaviour
{
    [SerializeField] public UnityEvent<CharacterDamage> onDeath;
    HurtBox hurtBox;
    bool isDead;
    float timeWhenLastPainWasReceived;
    bool hasBeenRecentlyHurt = false;

    private void Awake()
    {
        foreach (HurtBox h in GetComponentsInChildren<HurtBox>())
            { h.onHitNotified.AddListener(OnHitNotified); }
    }

    void OnHitNotified(HurtBox hurtBox, HitBox hitBox)
    {
        hasBeenRecentlyHurt = true;
        timeWhenLastPainWasReceived = Time.time;
        ProcessHit(hurtBox, hitBox);
    }

    protected virtual void ProcessHit(HurtBox hurtBox, HitBox hitBox)
    {
        Die();
    }

    protected virtual void Die()
    {
        isDead = true;
        onDeath.Invoke(this);
    }

    public virtual bool IsDead() { return isDead; }
    public virtual bool HasBeenHit() 
    {
        bool hasBeenHit = hasBeenRecentlyHurt;
        hasBeenRecentlyHurt = false;
        return hasBeenHit;
    }

    private void OnDestroy()
    {
        foreach (HurtBox h in GetComponentsInChildren<HurtBox>())
            { h.onHitNotified.RemoveListener(OnHitNotified); }
    }
}
