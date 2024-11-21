using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
    [Header("Movement")]
    public float horizontal;
    public float vertical;

    [Header("Base Stat Settings")]
    [SerializeField] protected float entitySpeed = 5f;

    [Header("Direction")]
    public Vector3 entityDirection;

    [Header("Base Settings")]
    protected float turnSmoothVelocity;
    [SerializeField] protected float turnSmoothTime = 0.1f;

    public abstract void MoveLogic();
}
