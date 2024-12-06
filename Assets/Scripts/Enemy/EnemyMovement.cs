using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : BaseMovement
{
    #region Events

    public Action<float> OnEnemyRotationChange;
    public Action<float> OnEnemyRawRotationChange;

    #endregion

    #region Variables

    [Header("Enums")]
    public EnemyMoveState enemyMoveState;

    [Header("Transforms")]
    [SerializeField] private Transform _spriteTransform;

    #endregion

    private GameObject _rootObj;

    void Awake()
    {
        _rootObj = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        hurtboxTransform = _rootObj.transform.Find("Hurtbox");
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveLogic();
    }

    public override void MoveLogic()
    {
        base.MoveLogic();

        if (moveDirection.magnitude > 0.1f)
        {
            enemyMoveState = EnemyMoveState.Moving;

            OnEnemyRawRotationChange?.Invoke(targetAngle);
            OnEnemyRotationChange?.Invoke(smoothAngle);
        }
        else
        {
            enemyMoveState = EnemyMoveState.Idle;
        }
    }
}

public enum EnemyMoveState
{
    Idle, Moving,
}
