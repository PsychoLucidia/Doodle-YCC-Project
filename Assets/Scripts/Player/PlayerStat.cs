using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerStat : BaseStat
{   
    public event Action<PauseState> OnLevelUp;
    public UnityEvent UnityOnLevelUp;

    [Header("Components")]
    public PlayerAttackHandler playerAttackHandler;
    public PlayerMovementCC playerMovementCC;

    [Header("Player EXP")]
    public int initialExp;
    public int currentExp;
    public int expToNextLevel;

    [Header("XP Multiplier Settings")]
    [SerializeField] private float expMultiplier = 0.07f;
    [SerializeField] private float expLevelMultiplier = 0.3f;

    private int _previousDamage = 0;
    private int _previousHealth = 0;
    private float _previousSpeed = 0;

    private bool _gameStarted = false;

    [Header("Booleans")]
    public bool isDead = false;

    void Awake()
    {
        RecalculateDamage();
        RecalculateHealth();
    }

    void Start()
    {
        expToNextLevel = initialExp;
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
        health -= damage;

        if (health <= 0)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    void ValueTracker()
    {
        CheckDamage();
        CheckHealth();
        CheckSpeed();
        CheckExperience();
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

    /// <summary>
    /// Checks the current experience and updates the player's level
    /// if the experience exceeds the experience required to reach the next level.
    /// </summary>
    void CheckExperience()
    {
        // Check if the player has enough experience to level up
        if (currentExp >= expToNextLevel)
        {
            // If the player has reached the maximum level, do not level up further
            if (level > maxLevel)
            {
                level = maxLevel;
                return;
            }

            // Level up the player and update the experience
            level++;
            currentExp -= expToNextLevel;
            expToNextLevel = RecalculateExperience();
            OnLevelUp?.Invoke(PauseState.Paused);
            UnityOnLevelUp?.Invoke();
        }
    }

    /// <summary>
    /// Calculates the experience required to reach the next level based on the player's current level.
    /// </summary>
    /// <returns>The experience required to reach the next level.</returns>
    int RecalculateExperience()
    {
        // The initial value is the previous experience required to reach the next level
        int initialValue = expToNextLevel;

        // Calculate the value from the multiplier
        float valueFromMultiplier = initialValue * expMultiplier;

        // Calculate the value from the player's current level
        float valueFromLevel = level * expLevelMultiplier;

        // Add the two values and convert to an integer
        int convertedValue = Mathf.RoundToInt(valueFromMultiplier + valueFromLevel);

        // Return the total experience required to reach the next level
        return initialValue + convertedValue;
    }

    /// <summary>
    /// Checks and updates the player's health based on recalculated values.
    /// If the game hasn't started, sets the initial health. Adjusts health
    /// and maxHealth by the difference between new and previous health values.
    /// Ensures health does not fall below 1 when decremented.
    /// </summary>
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

    void CheckSpeed()
    {
        float newSpeed = RecalculateSpeed();
        if (_previousSpeed != newSpeed)
        {
            speed = newSpeed;

            _previousSpeed = newSpeed;
        }

        if (playerMovementCC.entitySpeed != speed)
        {
            playerMovementCC.entitySpeed = speed;
        }
    }

    /// <summary>
    /// Recalculates the total damage based on the player's current
    /// weapon and additional damage.
    /// </summary>
    /// <returns>The total damage as an integer.</returns>
    int RecalculateDamage()
    {
        // Get the initial damage from the player's current weapon
        int weaponDamage = playerAttackHandler.weaponStat.initialDamage;

        // Get the additional damage from the player's stats
        int playerDamage = additionalDamage;

        // Calculate the total damage by adding the weapon damage and player damage
        int finalDamage = weaponDamage + playerDamage;

        // Return the total damage
        return finalDamage;
    }

    int RecalculateHealth()
    {
        return initialHealth + additionalHealth;
    }

    float RecalculateSpeed()
    {
        return initialSpeed + additionalSpeed;
    }
}
