using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : BaseHurtbox, IDamageable
{
    [SerializeField] private PlayerStat _playerStat;

    void Awake()
    {
        _playerStat = GetComponentInParent<PlayerStat>();
    }

    protected override int CalculateDamage(int inputDamage)
    {
        // Formula Work in Progress

        return inputDamage;
    }

    public void TakeDamage(int damage)
    {
        _playerStat.TakeDamage(CalculateDamage(damage));
    }
}
