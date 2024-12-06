using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : BaseObjectPool
{
    public float additionalHealthMultiplier = 0.15f;

    public PlayerStat playerStat;

    private ParticleSpawner _particleSpawner;
    void Awake()
    {
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();

        ComponentInitialization();
        Initialization();
    }

    /// <summary>
    /// Activates an enemy object from the pool at the specified spawn position with attributes
    /// adjusted based on the player's level. If no inactive objects are available and pool size
    /// is not limited, a new object is instantiated.
    /// </summary>
    /// <param name="playerLevel">The level of the player, used to scale enemy attributes.</param>
    /// <param name="spawnPosition">The position in the world to spawn the enemy object.</param>
    public override void ActivateObject(int playerLevel, Vector3 spawnPosition)
    {
        if (inactiveObjects.Count > 0)
        {
            GameObject obj = inactiveObjects[0];
            EnemyStat stat = obj.GetComponent<EnemyStat>();

            obj.transform.position = spawnPosition;

            stat.enemyPool = this;
            stat.maxHealth = stat.initialHealth + SetAdditionalHealth(playerLevel);
            stat.health = stat.maxHealth;
            stat.speed = stat.initialSpeed + SetSpeedMultiplier(playerLevel);
            stat.damage = stat.initialDamage + SetDamageMultiplier(playerLevel);
            stat.xpDrop = stat.initalExpDrop;
            stat.level = Mathf.RoundToInt(GameManager.Instance.difficultyLevel * 1.4f);

            obj.SetActive(true);
            inactiveObjects.RemoveAt(0);
            activeObjects.Add(obj);
        }
        else
        {
            if (!limitToPoolSize)
            {
                OnPoolExhausted(playerLevel, spawnPosition);
            }
            else
            {
                Debug.Log("Limit to pool size is true");
                return;
            }
        }
    }

    /// <summary>
    /// Handles the situation when the slime enemy pool is exhausted.
    /// Instantiates a new slime enemy object at the specified spawn position with the specified level.
    /// </summary>
    /// <param name="level">The level of the slime enemy to instantiate.</param>
    /// <param name="spawnPosition">The position to spawn the slime enemy in world space.</param>
    protected override void OnPoolExhausted(int level, Vector3 spawnPosition)
    {
        GameObject obj = Instantiate(objectPrefab, spawnPosition, Quaternion.identity, transform);
        obj.name = gameObject.name + activeObjects.Count;

        EnemyStat stat = obj.GetComponent<EnemyStat>();

        obj.transform.position = spawnPosition;

        stat.playerStat = playerStat;
        stat.enemyPool = this;
        stat.particleSpawner = _particleSpawner;
        stat.level = Mathf.RoundToInt(GameManager.Instance.difficultyLevel * 1.4f);
        stat.speed = stat.initialSpeed + SetSpeedMultiplier(level);
        stat.maxHealth = stat.initialHealth + SetAdditionalHealth(level);
        stat.health = stat.maxHealth;
        stat.xpDrop = stat.initalExpDrop;

        obj.SetActive(true);
        activeObjects.Add(obj);
    }

    /// <summary>
    /// Calculates the speed multiplier based on the player's level and game difficulty.
    /// </summary>
    /// <param name="level">The level of the player.</param>
    /// <returns>The calculated speed multiplier as a float.</returns>
    float SetSpeedMultiplier(int level)
    {
        // Base speed is derived from the game's difficulty level.
        int baseSpeed = GameManager.Instance.difficultyLevel - 1;

        // Calculate speed increment based on player level.
        float levelSpeed = level * 0.01f;

        // Return the total speed multiplier.
        return baseSpeed + (levelSpeed - 0.01f);
    }

    int SetDamageMultiplier(int level)
    {
        float diffLevelMultiplier = (GameManager.Instance.difficultyLevel - 1) * 1.6f;
        float levelMultiplier = level * 0.3f;

        return Mathf.RoundToInt(diffLevelMultiplier + levelMultiplier);
    }

    /// <summary>
    /// Calculates the additional health based on the player's level and game difficulty.
    /// </summary>
    /// <param name="level">The level of the player.</param>
    /// <returns>The calculated additional health as an integer.</returns>
    int SetAdditionalHealth(int level)
    {
        int baseHealth = (GameManager.Instance.difficultyLevel - 1) * 5;

        float levelHealth = level * additionalHealthMultiplier;

        int floatToInt = Mathf.RoundToInt(levelHealth);

        return baseHealth + floatToInt;
    }


    
    void Initialization()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab, transform.position, Quaternion.identity, transform);
            EnemyStat stat = obj.GetComponent<EnemyStat>();

            obj.name = gameObject.name + i;

            stat.playerStat = playerStat;
            stat.enemyPool = this;
            stat.particleSpawner = _particleSpawner;

            obj.SetActive(false);
            inactiveObjects.Add(obj);
        }
    }

    void ComponentInitialization()
    {
        _particleSpawner = GameObject.FindGameObjectWithTag("ParticleSpawner").GetComponent<ParticleSpawner>();
    }
}
