using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : BaseAnimation
{
    [Header("Sub Components")]
    [SerializeField] private PlayerMovementCC playerMovementCC;
    [SerializeField] private PlayerAttackHandler playerAttackHandler;

    Coroutine attackSequence;


    void Update()
    {
        ChangeAnimation();
    }

    void OnEnable()
    {
        playerMovementCC.OnPlayerRawRotationChange += FlipSprite;
    }

    void OnDisable()
    {
        playerMovementCC.OnPlayerRawRotationChange -= FlipSprite;
    }

    /// <summary>
    /// Changes the player's animation based on their movement state.
    /// </summary>
    /// <remarks>
    /// If the player is not attacking, the animation is changed based on
    /// whether the player is moving or not. If the player is attacking,
    /// the attack sequence is started. If the attack sequence is already
    /// in progress, the animation state is not changed.
    /// </remarks>
    void ChangeAnimation()
    {
        if (playerAttackHandler.playerAttackState != PlayerAttackState.Attacking)
        {
            switch (playerMovementCC.playerState)
            {
                case PlayerMoveState.Idle:
                    MoveAnimationStateChange(idleAnimationName);
                    break;
                case PlayerMoveState.Moving:
                    MoveAnimationStateChange(moveAnimationName);
                    break;
            }
        }
        else
        {
            if (!PlayerAttackHandler.isAttacking)
            {
                PlayerAttackHandler.isAttacking = true;
                attackSequence = StartCoroutine(AttackSequence());
            }
        }
    }

    /// <summary>
    /// Changes the player's animation state to the given state.
    /// </summary>
    /// <param name="state">The name of the state to change to.</param>
    /// <remarks>
    /// If the player is already in the given state, this method does nothing.
    /// </remarks>
    void MoveAnimationStateChange(string state)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(state)) { return; }
        animator.Play(state);
    }

    /// <summary>
    /// Converts the given number of frames into a float value in seconds,
    /// given the specified frames per second.
    /// </summary>
    /// <param name="frames">The number of frames to convert.</param>
    /// <param name="fps">The frames per second.</param>
    /// <returns>The given number of frames in seconds.</returns>
    public float FramesToFloat(int frames, int fps)
    {
        return frames / fps;
    }

    /// <summary>
    /// Waits for the duration of the attack animation to end and then sets the
    /// PlayerAttackHandler's state back to None.
    /// </summary>
    IEnumerator AttackSequence()
    {
        animator.Play(attackAnimationName);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimationName)) { yield return null; }
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        PlayerAttackHandler.isAttacking = false;
        playerAttackHandler.playerAttackState = PlayerAttackState.None;
        attackSequence = null;
    }
}
