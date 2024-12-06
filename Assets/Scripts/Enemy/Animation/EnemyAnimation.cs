using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : BaseAnimation
{
    private EnemyMovement _enemyMovement;

    void OnEnable()
    {
        if (_enemyMovement == null)
        {
            _enemyMovement = GetComponentInParent<EnemyMovement>();
        }

        if (_enemyMovement != null)
        {
            _enemyMovement.OnEnemyRawRotationChange += FlipSprite;
            _enemyMovement.OnEnemyRawRotationChange += RotateSprite;

            Debug.Log("OnEnable");
        }
    }

    void OnDisable()
    {
        _enemyMovement.OnEnemyRawRotationChange -= FlipSprite;
        _enemyMovement.OnEnemyRawRotationChange -= RotateSprite;
    }
}
