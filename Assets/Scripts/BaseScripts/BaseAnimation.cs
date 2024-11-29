using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimation : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected bool enableFlip = true;

    [Header("Base Components")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    [Header("Animation States")]
    [SerializeField] protected string idleAnimationName;
    [SerializeField] protected string moveAnimationName;
    [SerializeField] protected string attackAnimationName;

    protected virtual void FlipSprite(float angle)
    {
        if (enableFlip)
        {
            if (angle > 90 && angle <= 180 || angle < -90 && angle >= -180)
            {
                spriteRenderer.flipX = true;
            }
            else if (angle < 90 && angle >= 0 || angle > -90 && angle <= 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
