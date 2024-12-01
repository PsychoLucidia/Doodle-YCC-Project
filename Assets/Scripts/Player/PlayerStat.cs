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

    void ClampLife()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}
