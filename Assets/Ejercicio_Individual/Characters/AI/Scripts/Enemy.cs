using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour, IVelocityReadable
{
    [SerializeField] Transform target;

    [SerializeField] float walkSpeed = 1f;
    [SerializeField] float runSpeed = 2f;
    [SerializeField] float jumpSpeed = 2f;

    [SerializeField] float meleeDistance = 1f;

    NavMeshAgent navMeshAgent;
    EnemyCombat enemyCombat;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyCombat = GetComponent<EnemyCombat>();
    }

    void Update()
    {
        if (target)
        {
            if (Vector3.Distance(transform.position, target.position) > meleeDistance)
            {
                enemyCombat.StopCombat();
                navMeshAgent.destination = target.position; 
            }
            else
                { enemyCombat.StartCombat(); }
        }

    }


    #region IVelocityReadable
    Vector3 IVelocityReadable.GetVelocity() { return navMeshAgent.velocity; }

    float IVelocityReadable.GetWalkSpeed() { return walkSpeed; }
    float IVelocityReadable.GetRunSpeed() { return runSpeed; }

    float IVelocityReadable.GetJumpSpeed() { return jumpSpeed; }

    bool IVelocityReadable.IsGrounded() { return true; }
    #endregion
}
