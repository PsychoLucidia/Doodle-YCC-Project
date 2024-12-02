using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat
{
    [Header("Player EXP")]
    public int currentExp;
    public int expToNextLevel;

    void Update()
    {
        ClampLife();
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
}
