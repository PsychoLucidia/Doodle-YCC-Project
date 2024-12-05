using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : BaseAnimation
{
    [SerializeField] EnemyMovement enemyMovement;
    void OnEnable()
    {
        if (enemyMovement != null)
        {
            enemyMovement.OnEnemyRawRotationChange += FlipSprite;
            Debug.Log("OnEnable");
        }
    }

    void OnDisable()
    {
        if (enemyMovement != null)
        {
            enemyMovement.OnEnemyRawRotationChange -= FlipSprite;
        }
    }
}
