using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyPool : BaseObjectPool
{
    void Awake()
    {
        Initialization();
    }

    public override void ActivateObject(int playerLevel, Vector3 spawnPosition)
    {
        if (inactiveObjects.Count > 0)
        {
            GameObject obj = inactiveObjects[0];
            EnemyStat stat = obj.GetComponent<EnemyStat>();

            obj.transform.position = spawnPosition;

            stat.level = Mathf.RoundToInt(GameManager.Instance.difficultyLevel * 1.4f);
            stat.speed = stat.initialSpeed;
            stat.additionalSpeed = SetSpeedMultiplier(playerLevel);
            stat.maxHealth = stat.initialHealth;
            stat.additionalHealth = SetAdditionalHealth(playerLevel);


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

    protected override void OnPoolExhausted(int level, Vector3 spawnPosition)
    {
        GameObject obj = Instantiate(objectPrefab, spawnPosition, Quaternion.identity, transform);
        obj.name = gameObject.name + activeObjects.Count;

        EnemyStat stat = obj.GetComponent<EnemyStat>();

        obj.transform.position = spawnPosition;

        stat.level = Mathf.RoundToInt(GameManager.Instance.difficultyLevel * 1.4f);
        stat.speed = stat.initialSpeed;
        stat.additionalSpeed = SetSpeedMultiplier(level);

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
    int baseSpeed = GameManager.Instance.difficultyLevel;

    // Calculate speed increment based on player level.
    float levelSpeed = level * 0.01f;

    // Return the total speed multiplier.
    return baseSpeed + (levelSpeed - 0.01f);
}

    int SetAdditionalHealth(int level)
    {
        int baseHealth = GameManager.Instance.difficultyLevel - 0;
        float levelHealth = level * .01f;

        int floatToInt = Mathf.RoundToInt(levelHealth);
        return 0;
    }


    
    void Initialization()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(objectPrefab, transform.position, Quaternion.identity, transform);

            obj.name = gameObject.name + i;

            obj.SetActive(false);
            inactiveObjects.Add(obj);
        }
    }
}
