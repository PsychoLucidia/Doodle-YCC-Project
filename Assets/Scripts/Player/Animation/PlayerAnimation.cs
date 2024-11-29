using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : BaseAnimation
{
    [Header("Sub Components")]
    [SerializeField] PlayerMovementCC playerMovementCC;
    [SerializeField] PlayerAttackHandler playerAttackHandler;

    Coroutine attackSequence;

    void Update()
    {
        ChangeAnimation();
        Debug.Log("Attack Animation Length: " + animator.GetCurrentAnimatorStateInfo(0).length);
    }

    void OnEnable()
    {
        PlayerMovementCC.OnPlayerRawRotationChange += FlipSprite;
    }

    void OnDisable()
    {
        PlayerMovementCC.OnPlayerRawRotationChange -= FlipSprite;
    }

    /// <summary>
    /// Changes the player's animation state based on their current state
    /// </summary>
    /// <remarks>
    /// If the player is not attacking, change the animation based on the player's state.
    /// If the player is attacking, play the attack animation and wait for the animation to end
    /// to set the player's attack state back to None.
    /// </remarks>
    void ChangeAnimation()
    {
        if (playerAttackHandler.playerAttackState != PlayerAttackState.Attacking)
        {
            switch (playerMovementCC.playerState)
            {
                case PlayerMoveState.Idle:
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("GogoMove")) { animator.Play("GogoIdle"); }
                    break;
                case PlayerMoveState.Moving:
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("GogoIdle")) { animator.Play("GogoMove"); }
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
        animator.Play("GogoAttack");

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("GogoAttack")) { yield return null; }
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        PlayerAttackHandler.isAttacking = false;
        playerAttackHandler.playerAttackState = PlayerAttackState.None;
        attackSequence = null;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("GogoAttack"))
        {
            switch (playerMovementCC.playerState)
            {
                case PlayerMoveState.Idle:
                    animator.Play("GogoIdle");
                    break;
                case PlayerMoveState.Moving:
                    animator.Play("GogoMove");
                    break;
            }
        }
    }
}
