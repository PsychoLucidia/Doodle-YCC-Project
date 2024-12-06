using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : BaseMovement
{
    #region Events

    public Action<float> OnPlayerRotationChange; // Called when the player's rotation changes
    public Action<float> OnPlayerRawRotationChange;

    #endregion    

    #region Variables

    [Header("Enums")]
    public PlayerMoveState playerState;

    // Non-serialized
    private GameObject _rootObj;

    #endregion

    void Awake()
    {
        _rootObj = this.gameObject;
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
        hurtboxTransform = _rootObj.transform.Find("Hurtbox");

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
    /// Updates player state and invokes rotation change events.
    /// </summary>
    public override void MoveLogic()
    {
        // Call the base class's MoveLogic method for shared movement logic.
        base.MoveLogic();

        // Check if the player is moving based on movement direction magnitude.
        if (moveDirection.magnitude > 0.1f)
        {
            // Invoke event for smooth angle rotation change.
            OnPlayerRotationChange?.Invoke(smoothAngle);

            // Invoke event for raw target angle rotation change.
            OnPlayerRawRotationChange?.Invoke(targetAngle);

            // Set player state to moving.
            playerState = PlayerMoveState.Moving;
        }
        else
        {
            // Set player state to idle if not moving.
            playerState = PlayerMoveState.Idle;
        }
    }
}

public enum PlayerMoveState
{
    Idle, Moving, 
}
