using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int initialSpawnCount = 2;
    public float initialInterval = 4f;
    public float spawnOffset = 1f;

    [SerializeField] int spawnCount;
    [SerializeField] float spawnInterval;

    public SpawnTable[] spawnTable;
    public BaseObjectPool[] objectPools;

    public int spawnTableIncrement = 0;

    public int[] enemyIDs;

    float _timer = 0f;

    PlayerStat playerStat;

    void Awake()
    {
        BaseObjectPool[] pools = GetComponentsInChildren<BaseObjectPool>();

        objectPools = pools;

        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = initialSpawnCount;
        spawnInterval = initialInterval;

        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer();
    }

    void SpawnEnemy()
    {
        Vector2 screenMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        int randomSides = Random.Range(0, 3);
        Vector3 spawnPos = Vector3.zero;

        switch (randomSides)
        {
            case 0: spawnPos = new Vector3(Random.Range(screenMin.x, screenMax.x), screenMax.y + spawnOffset, 0); break; // Top
            case 1: spawnPos = new Vector3(Random.Range(screenMin.x, screenMax.x), screenMin.y - spawnOffset, 0); break; // Bottom
            case 2: spawnPos = new Vector3(screenMax.x + spawnOffset, Random.Range(screenMin.y, screenMax.y), 0); break; // Left
            case 3: spawnPos = new Vector3(screenMin.x - spawnOffset, Random.Range(screenMin.y, screenMax.y), 0); break; // Right
            default: break; // Error
        }

        for (int i = 0; i < spawnCount; i++)
        {
            enemyIDs = spawnTable[0].spawnIDTable;

            objectPools[enemyIDs[Random.Range(0, enemyIDs.Length)]].ActivateObject(SetEnemyLevel(), spawnPos);
        }

    }

    void TableTracker()
    {
        if (_timer > spawnTable[spawnTableIncrement].tableChangeTime)
        {
            spawnTableIncrement++;
            

            _timer = 0;
        }
    }

    void SpawnTimer()
    {
        _timer += Time.deltaTime;

        if (_timer > spawnInterval)
        {
            SpawnEnemy();
            _timer = 0f;
        }
    }

    
    /// <summary>
    /// Sets the enemy level to be spawned, based on the current player level.
    /// If the playerStat is not found, the enemy level will be set to 1.
    /// </summary>
    /// <returns>The level of the enemy to be spawned.</returns>
    int SetEnemyLevel()
    {
        int finalLevel = 1;
        if (playerStat != null)
        {
            finalLevel = Random.Range(playerStat.level, playerStat.level + 1);
        }

        return finalLevel;
    }
}

[System.Serializable]
public class SpawnTable
{
    public int[] spawnIDTable;
    public float tableChangeTime;
    public int spawnCount;
    public float spawnInterval;
}
