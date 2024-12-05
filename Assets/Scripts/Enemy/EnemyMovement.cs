using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : BaseMovement
{
    #region Events

    public Action<float> OnEnemyRawRotationChange;

    #endregion

    #region Variables

    [Header("Enums")]
    public EnemyMoveState enemyMoveState;

    [Header("Private Variables")]
    [SerializeField] Transform playerTransform;

    #endregion

    GameObject _rootObj;

    void Awake()
    {
        _rootObj = this.gameObject;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
