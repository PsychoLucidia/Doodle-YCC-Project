using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStat : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public int maxHealth;
    public int speed;
    public int damage;
    public int defense;

    [Header("Level")]
    public int level;
    public int maxLevel;
}
