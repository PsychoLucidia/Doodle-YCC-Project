using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : BaseMovement
{
    #region Events

    public static Action<float> OnPlayerRotationChange; // Called when the player's rotation changes
    public static Action<float> OnPlayerRawRotationChange;

    #endregion    

    #region Variables

    [Header("Public Components")]
    public CharacterController characterController;
    public Transform hurtboxTransform;

    [Header("Enums")]
    public PlayerState playerState;
    public PlayerAttackState playerAttackState;


    // Non-serialized
    GameObject rootObj;

    #endregion

    void Awake()
    {
        rootObj = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    /// <summary>
    /// Initializes all components and variables that are needed for the player's movement.
    /// </summary>
    void Initialization()
    {
        // Find the hurtbox, which is used to detect collisions with enemies and other objects that can harm the player.
        hurtboxTransform = rootObj.transform.Find("Hurtbox");

        // Get the CharacterController component, which is used to move the player.
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveLogic();
    }

    /// <summary>
    /// Main logic for moving the player.
    /// </summary>
    public override void MoveLogic()
    {
        // Get the direction of the player's movement
        Vector3 direction = new Vector3(horizontal, vertical, 0);

        // If the player is moving
        if (direction.magnitude > 0.1f)
        {
            // Calculate the angle that the player should be facing
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Smoothly turn the player towards the target angle
            float smoothAngle = Mathf.SmoothDampAngle(hurtboxTransform.eulerAngles.z, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            hurtboxTransform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);

            // Move the player in the direction they are facing
            Vector3 moveDir = Quaternion.Euler(0f, 0f, targetAngle) * Vector3.right;

            // Notify any listeners that the player has changed direction
            OnPlayerRotationChange?.Invoke(smoothAngle);
            OnPlayerRawRotationChange?.Invoke(targetAngle);

            // Set the player's state to moving
            playerState = PlayerState.Moving;

            // Move the player
            characterController.Move(entitySpeed * Time.deltaTime * moveDir);
        }
        else
        {
            // Set the player's state to idle
            playerState = PlayerState.Idle;
        }
    }
}

public enum PlayerState
{
    Idle, Moving, 
}

public enum PlayerAttackState
{
    None, Attacking,
}
