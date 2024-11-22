using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimation : MonoBehaviour
{
    [Header("Base Components")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        PlayerMovementCC.OnPlayerRawRotationChange += FlipSprite;
    }

    void OnDisable()
    {
        PlayerMovementCC.OnPlayerRawRotationChange -= FlipSprite;
    }

    protected virtual void FlipSprite(float angle)
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
