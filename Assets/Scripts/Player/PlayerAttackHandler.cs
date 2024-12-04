using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackHandler : MonoBehaviour
{
    #region Events

    public static Action<int> OnSendAttackDamage;
    public static Action OnPlayerAttack;

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
