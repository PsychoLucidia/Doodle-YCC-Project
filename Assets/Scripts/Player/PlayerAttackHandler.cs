using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackHandler : MonoBehaviour
{
    #region Events

    public Action<int> OnSendAttackDamage;
    public Action OnPlayerAttack;

    public UnityEvent OnAttackStart;

    #endregion

    #region Variables

    [Header("Public Variables")]
    public static bool isAttacking;
    public PlayerAttackState playerAttackState;
    public WeaponStat weaponStat;
    public PlayerStat playerStat;
    
    // Non-serialized
    bool _isAttacking;

    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        _isAttacking = isAttacking;
    }

    public void PlayerAttack()
    {
        playerAttackState = PlayerAttackState.Attacking;
        OnSendAttackDamage?.Invoke(CalculateAttackDamage());
        OnPlayerAttack?.Invoke();
        OnAttackStart?.Invoke();
    }

    int CalculateAttackDamage()
    {
        if (weaponStat == null) { return 0; }
        
        // Formula is currently working in progress
        float attackDamage = playerStat.damage;

        return Mathf.RoundToInt(attackDamage);
    }
}

public enum PlayerAttackState
{
    None, Attacking,
}
