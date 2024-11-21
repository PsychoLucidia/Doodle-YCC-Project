using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStat : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public int maxHealth;

    [Header("Level")]
    public int level;
    public int maxLevel;
    public int currentExp;
    public int expToNextLevel;
}
