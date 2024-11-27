using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : BaseAnimation
{
    [Header("Sub Components")]
    [SerializeField] PlayerMovementCC playerMovementCC;
    [SerializeField] PlayerAttackHandler playerAttackHandler;

    void Update()
    {
        ChangeAnimation();
    }

    void OnEnable()
    {
        PlayerMovementCC.OnPlayerRawRotationChange += FlipSprite;
    }

    void OnDisable()
    {
        PlayerMovementCC.OnPlayerRawRotationChange -= FlipSprite;
    }

    void ChangeAnimation()
    {
        if (playerAttackHandler.playerAttackState != PlayerAttackState.Attacking)
        {
            switch (playerMovementCC.playerState)
            {
                case PlayerState.Idle:
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("GogoMove")) { animator.Play("GogoIdle"); }
                    break;
                case PlayerState.Moving:
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("GogoIdle")) { animator.Play("GogoMove"); }
                    break;
            }
        }
        else
        {
            if (!playerAttackHandler.isAttacking)
            {
                playerAttackHandler.isAttacking = true;
                animator.Play("GogoAttack");
            }
        }
    }

    IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(0.5f);
        playerAttackHandler.isAttacking = false;
    }
}
