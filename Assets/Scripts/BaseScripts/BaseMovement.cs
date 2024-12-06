using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
    [Header("Movement")]
    public float horizontal;
    public float vertical;

    [Header("Base Stat Settings")]
    public float entitySpeed = 5f;

    [Header("Direction")]
    public Vector3 entityDirection;

    [Header("Base Settings")]
    protected float turnSmoothVelocity;
    [SerializeField] protected float turnSmoothTime = 0.1f;

    [Header("Base Public Components")]
    public CharacterController characterController;
    public Transform hurtboxTransform;

    // Non-serialized
    protected Vector3 moveDirection;

    protected float targetAngle;
    protected float smoothAngle;

    public virtual void MoveLogic()
    {
        moveDirection = new Vector3(horizontal, vertical, 0);

        if (moveDirection.magnitude > 0.1f)
        {
            targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            smoothAngle = Mathf.SmoothDampAngle(hurtboxTransform.eulerAngles.z, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            hurtboxTransform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);

            Vector3 moveDir = Quaternion.Euler(0f, 0f, targetAngle) * Vector3.right;

            characterController.Move(entitySpeed * Time.deltaTime * moveDir);
        }
    }

}
