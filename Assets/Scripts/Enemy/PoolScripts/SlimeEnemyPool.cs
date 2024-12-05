using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    float SetSpeedMultiplier(int level)
    {
        int baseSpeed = GameManager.Instance.difficultyLevel - 0;
        float levelSpeed = level * 0.01f;

        float finalSpeed = baseSpeed + (levelSpeed - 0.01f);
        return finalSpeed;
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
