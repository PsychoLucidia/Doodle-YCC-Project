using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : BaseAnimation
{
    void OnEnable()
    {
        EnemyMovement.OnEnemyRawRotationChange += FlipSprite;
    }

    void OnDisable()
    {
        EnemyMovement.OnEnemyRawRotationChange -= FlipSprite;
    }
}
