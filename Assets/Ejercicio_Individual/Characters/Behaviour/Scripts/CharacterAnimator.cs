using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] float smoothingSpeed = 4f;
    Animator animator;
    IVelocityReadable velocityReadable;
    IAttackReadable attackReadable;
    CharacterDamage characterDamage;

    Vector3 oldLocalVelocity;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        velocityReadable = GetComponent<IVelocityReadable>();
        characterDamage = GetComponent<CharacterDamage>();
        attackReadable = GetComponent<IAttackReadable>();
    }

    private void Update()
    {
        Vector3 velocity = velocityReadable.GetVelocity();
        Vector3 currentLocalVelocity = transform.InverseTransformDirection(velocity);

        float verticalVelocity = currentLocalVelocity.y;
        currentLocalVelocity.y = 0f;

        Vector3 direction = currentLocalVelocity - oldLocalVelocity;
        float distance = direction.magnitude;
        Vector3 smoothedLocalVelocity = oldLocalVelocity + direction.normalized * Mathf.Min(distance, smoothingSpeed * Time.deltaTime);
        oldLocalVelocity = smoothedLocalVelocity;

        float normalizedSmoothedLocalSpeed = 0f;

        float planeSpeed = smoothedLocalVelocity.magnitude;
        if (planeSpeed < velocityReadable.GetWalkSpeed()) 
        {
            normalizedSmoothedLocalSpeed = Mathf.Lerp(0f, 1f, planeSpeed / velocityReadable.GetWalkSpeed());
        }
        else
        {
            float speedOverWalkSpeed = planeSpeed - velocityReadable.GetWalkSpeed();
            normalizedSmoothedLocalSpeed = Mathf.Lerp(1f, 2f, speedOverWalkSpeed / (velocityReadable.GetRunSpeed() - velocityReadable.GetWalkSpeed()));
        }

        Vector3 normalizedSmoothedLocalVelocity = smoothedLocalVelocity.normalized * normalizedSmoothedLocalSpeed;

        animator.SetFloat("ForwardVelocity", normalizedSmoothedLocalVelocity.z);
        animator.SetFloat("SidewardVelocity", normalizedSmoothedLocalVelocity.x);

        animator.SetFloat("VerticalVelocity", verticalVelocity);
        animator.SetFloat("VerticalVelocityNormalized", 
            velocityReadable.IsGrounded() ? 0f :
            InverseLerpUnclamped(velocityReadable.GetJumpSpeed(), -velocityReadable.GetJumpSpeed(), verticalVelocity)
            );

        animator.SetBool("IsGrounded", velocityReadable.IsGrounded());

        if (characterDamage)
        {
            animator.SetBool("Dead", characterDamage.IsDead());
            if (characterDamage.HasBeenHit()) { animator.SetTrigger("HasBeenHit"); }
        }

        if (attackReadable != null)
        {
            if (attackReadable.CheckAttack()) { animator.SetTrigger("Attack"); }
        }
    }

    static float InverseLerpUnclamped(float min, float max, float value)
    {
        if (Mathf.Abs(max - min) < 0.0001f) return min;
        return (value - min) / (max - min);
    }
}
