using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectEXP {}

public abstract class BaseStat : MonoBehaviour
{
    [Header("Initial Values")]
    public int initialHealth;
    public int initialDamage;
    public int initialDefense;
    public float initialSpeed;

    [Header("Stats")]
    public int health;
    public int maxHealth;
    public int damage;
    public int defense;
    public float speed;

    [Header("Base")]
    public int additionalDamage;
    public int additionalHealth;
    public float additionalSpeed;

    [Header("Level")]
    public int level;
    public int maxLevel;

    public abstract void TakeDamage(int damage);
}
 