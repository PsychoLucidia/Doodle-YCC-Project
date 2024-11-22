using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : BaseAnimation
{
    [Header("Sub Components")]
    [SerializeField] PlayerMovementCC playerMovementCC;

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
        switch (playerMovementCC.playerState)
        {
            case PlayerState.Idle:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("GogoMove"))
                {
                    animator.Play("GogoIdle");
                }
                break;
            case PlayerState.Moving:
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("GogoIdle"))
                {
                    animator.Play("GogoMove");
                }
                break;
        }
    }
}
