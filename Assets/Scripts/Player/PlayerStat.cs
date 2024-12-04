using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat
{   
    [Header("Components")]
    public PlayerAttackHandler playerAttackHandler;
    public PlayerMovementCC playerMovementCC;

    [Header("Player EXP")]
    public int currentExp;
    public int expToNextLevel;

    int _previousDamage = 0;
    int _previousHealth = 0;

    bool _gameStarted = false;

    void Awake()
    {
        RecalculateDamage();
        RecalculateHealth();
    }

    void Update()
    {
        ClampLife();
        ValueTracker();
    }

    /// <summary>
    /// Clamps the player's health within the valid range from 0 to maxHealth.
    /// </summary>
    void ClampLife()
    {
        // Ensure the health value stays between 0 and maxHealth
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public override void TakeDamage(int damage)
    {

    }

    void ValueTracker()
    {
        CheckDamage();
        CheckHealth();
    }

    void CheckDamage()
    {
        int newDamage = RecalculateDamage();
        if (_previousDamage != newDamage)
        {
            damage = newDamage;
            _previousDamage = newDamage;
        }
    }

    void CheckHealth()
    {
        int newHealth = RecalculateHealth();
        if (_previousHealth != newHealth)
        {
            if (!_gameStarted)
            {
                health = newHealth;
                _gameStarted = true;
            }

            int incrementedHealth = newHealth - maxHealth;
            maxHealth = newHealth;

            if (incrementedHealth > 0)
            {
                health += incrementedHealth;
            }
            else if (incrementedHealth < 0)
            {
                int healthCheck = health + incrementedHealth;

                if (healthCheck <= 0)
                {
                    health = 1;
                }
                else
                {
                    health += incrementedHealth;
                }
            }

            _previousHealth = newHealth;
        }
    }

    int RecalculateDamage()
    {
        int weaponDamage = playerAttackHandler.weaponStat.initialDamage;
        int playerDamage = additionalDamage;

        int finalDamage = weaponDamage + playerDamage;

        return finalDamage;
    }

    int RecalculateHealth()
    {
        return initialHealth + additionalHealth;
    }
}
