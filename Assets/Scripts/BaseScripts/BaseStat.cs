using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStat : MonoBehaviour
{
    [Header("Initial Values")]
    public int initialHealth;
    public int initialSpeed;
    public int initialDamage;
    public int initialDefense;

    [Header("Stats")]
    public int health;
    public int maxHealth;
    public int speed;
    public int damage;
    public int defense;

    [Header("Base")]
    public int additionalDamage;
    public int additionalHealth;

    [Header("Level")]
    public int level;
    public int maxLevel;

    public abstract void TakeDamage(int damage);
}

public interface ICollectEXP {}
 