using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IVelocityReadable
{
    [Header("Movement on Plane")]
    [SerializeField] float walkSpeed = 1f;
    [SerializeField] float runSpeed = 2f;
    [SerializeField] float acceleration = 1f;
    [SerializeField] float deceleration = 4f;
    public enum MovementMode
    {
        Local,
        RelativeToCamera,
    }
    [SerializeField] MovementMode movementMode = MovementMode.RelativeToCamera;
    [SerializeField] Transform movementCamera;
    [SerializeField] Transform movementTarget;

    [Header("Vertical Movement")]
    [SerializeField] float jumpSpeed = 50f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float maxUpwardsSpeed = Mathf.Infinity;
    [SerializeField] float maxDownwardsSpeed = Mathf.NegativeInfinity;

    [Header("Rotation")]
    [SerializeField] float rotationSpeed = 360f;
    [SerializeField] bool rotateOnlyWhenMoving = true;
    public enum OrientationMode
    {
        MovementForward,
        LookToTarget,
        CameraForward,
    }
    [SerializeField] OrientationMode orientationMode;
    [SerializeField] Transform orientationCamera;
    [SerializeField] Transform orientationTarget;

    Vector2 deviceInput;
    Vector3 rawInput;
    bool isRunning;
    bool mustJump;

    float desiredPlaneSpeed;
    float currentPlaneSpeed;
    Vector3 lastMovementDirectionOnPlane;

    float verticalSpeed;

    float desiredRotation;

    // Cached Components
    CharacterController characterController;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        lastMovementDirectionOnPlane = transform.forward;
    }

    void Update()
    {
        UpdatePlaneSpeed();
        UpdateRotation();
        UpdateVerticalSpeed();
        UpdateJump();
        UpdateMovement();
        if (characterController.isGrounded)
            { verticalSpeed = 0f; }
    }

    private void UpdatePlaneSpeed()
    {
        float maxSpeed = isRunning ? runSpeed : walkSpeed;
        desiredPlaneSpeed = rawInput.sqrMagnitude == 0f ? 0f : maxSpeed;

        if (currentPlaneSpeed < desiredPlaneSpeed)
            { currentPlaneSpeed += acceleration * Time.deltaTime; }
        else if (currentPlaneSpeed > desiredPlaneSpeed)
            { currentPlaneSpeed -= deceleration * Time.deltaTime; }

        currentPlaneSpeed = Mathf.Clamp(currentPlaneSpeed, 0f, maxSpeed);
    }

    void UpdateRotation()
    {
        if (!rotateOnlyWhenMoving || rawInput.sqrMagnitude > 0f)
        {
            Vector3 desiredForward = lastMovementDirectionOnPlane;
            switch (orientationMode)
            {
                case OrientationMode.MovementForward:
                    desiredForward = lastMovementDirectionOnPlane;
                    break;
                case OrientationMode.LookToTarget:
                    if (orientationTarget)
                        { desiredForward = orientationTarget.position - transform.position; }
                    break;
                case OrientationMode.CameraForward:
                    if (orientationCamera)
                        { desiredForward = orientationCamera.transform.forward; }
                    break;
            }
            desiredForward.y = 0f;

            float angularDistance = Vector3.SignedAngle(transform.forward, desiredForward, Vector3.up);
            Quaternion rotation = Quaternion.identity;
            if (angularDistance > 0f)
            {
                rotation = Quaternion.AngleAxis(Mathf.Min(rotationSpeed * Time.deltaTime, angularDistance), Vector3.up);
            }
            else if (angularDistance < 0f)
            {
                rotation = Quaternion.AngleAxis(Mathf.Max(-rotationSpeed * Time.deltaTime, angularDistance), Vector3.up);
            }

            transform.rotation = rotation * transform.rotation;
        }
    }

    void UpdateJump()
    {
        if (mustJump && characterController.isGrounded)
            { verticalSpeed = jumpSpeed; }
        mustJump = false;
    }

    private void UpdateVerticalSpeed()
    {
        verticalSpeed += gravity * Time.deltaTime;
        verticalSpeed = Mathf.Clamp(verticalSpeed, maxDownwardsSpeed, maxUpwardsSpeed);
    }

    private void UpdateMovement()
    {
        Vector3 velocity = GetVelocity();
        velocity = AdaptVelocityToGround(velocity);
        characterController.Move(velocity * Time.deltaTime);

        //Debug.DrawRay(transform.position, velocity);

        if (velocity.sqrMagnitude > 0f)
            { lastMovementDirectionOnPlane = velocity.normalized; }
    }

    void OnMove(InputValue inputValue)
    {
        deviceInput = inputValue.Get<Vector2>();
        rawInput = Vector3.zero;
        rawInput.x = deviceInput.x;
        rawInput.z = deviceInput.y;
    }

    void OnJump()
    {
        mustJump = true;
    }

    void OnRun(InputValue inputValue)
    {
        isRunning = inputValue.Get<float>() > 0f;
    }

    Vector3 GetVelocity()
    {
        Vector3 planeXZMovement = rawInput.sqrMagnitude == 0f ? lastMovementDirectionOnPlane : Vector3.zero;
        switch (movementMode)
        {
            case MovementMode.Local:
                planeXZMovement = Vector3.ProjectOnPlane(transform.TransformDirection(rawInput), Vector3.up);
                break;
            case MovementMode.RelativeToCamera:
                if (movementCamera)
                {
                    planeXZMovement = Vector3.ProjectOnPlane(movementCamera.transform.TransformDirection(rawInput), Vector3.up);
                    Debug.DrawRay(transform.position, planeXZMovement, Color.red);
                }
                break;
        }

        planeXZMovement *= currentPlaneSpeed;

        return planeXZMovement + (Vector3.up * verticalSpeed);
    }

    Vector3 AdaptVelocityToGround(Vector3 velocity)
    {
        Vector3 adaptedVelocity = velocity;
        if (characterController.isGrounded)
        {
            Vector3 XZVelocity = velocity;
            XZVelocity.y = 0f;

            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.01f, Vector3.down, out hit, 1f))
            {
                Quaternion directionRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                XZVelocity = directionRotation * XZVelocity;
                adaptedVelocity = XZVelocity + Vector3.up * velocity.y;
                adaptedVelocity.y += gravity / 2f;
            }
        }

        return adaptedVelocity;
    }

    Vector3 IVelocityReadable.GetVelocity()
    {
        return GetVelocity();
    }

    float IVelocityReadable.GetWalkSpeed() { return walkSpeed; }
    float IVelocityReadable.GetRunSpeed() { return runSpeed; }
    float IVelocityReadable.GetJumpSpeed() { return jumpSpeed; }
    bool IVelocityReadable.IsGrounded() { return characterController.isGrounded; }
}
