using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : BaseSpawner
{
    public float spawnOffset = 1f;

    [SerializeField] int spawnCount;
    [SerializeField] float spawnInterval;

    public SpawnTable[] spawnTable;

    public int spawnTableIncrement = 0;

    public int[] enemyIDs;

    private float _timer = 0f;
    private float _spawnTableTimer = 0f;

    private PlayerStat playerStat;

    void Awake()
    {
        BaseObjectPool[] pools = GetComponentsInChildren<BaseObjectPool>();

        objectPools = pools;

        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = spawnTable[0].spawnCount;
        spawnInterval = spawnTable[0].spawnInterval;
        enemyIDs = spawnTable[0].spawnIDTable;

        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer();
        TableTracker();
    }

    void SpawnEnemy()
    {
        Vector2 screenMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector3 spawnPos = Vector3.zero;


        for (int i = 0; i < spawnCount; i++)
        {
            int randomSides = Random.Range(0, 3);

            switch (randomSides)
            {
                case 0: spawnPos = new Vector3(Random.Range(screenMin.x, screenMax.x), screenMax.y + spawnOffset, 0); break; // Top
                case 1: spawnPos = new Vector3(Random.Range(screenMin.x, screenMax.x), screenMin.y - spawnOffset, 0); break; // Bottom
                case 2: spawnPos = new Vector3(screenMax.x + spawnOffset, Random.Range(screenMin.y, screenMax.y), 0); break; // Left
                case 3: spawnPos = new Vector3(screenMin.x - spawnOffset, Random.Range(screenMin.y, screenMax.y), 0); break; // Right
                default: break; // Error
            }

            objectPools[enemyIDs[Random.Range(0, enemyIDs.Length)]].ActivateObject(SetEnemyLevel(), spawnPos);
        }

    }

    /// <summary>
    /// Tracks and updates the current spawn table based on elapsed time.
    /// Increments the spawnTableIncrement index when the _spawnTableTimer exceeds
    /// the current table's change time, updating enemy IDs, spawn count, and interval.
    /// Resets the timer after updating to the next table.
    /// </summary>
    void TableTracker()
    {
        // Check if the timer has exceeded the current table's change time
        if (_spawnTableTimer > spawnTable[spawnTableIncrement].tableChangeTime)
        {
            // Increment the spawn table index
            spawnTableIncrement++;

            // Ensure the index does not exceed the spawn table length
            if (spawnTableIncrement >= spawnTable.Length)
            {
                spawnTableIncrement--; // Revert to the last valid index
            }
            else
            {
                // Update enemy IDs, spawn count, and interval from the new table
                spawnCount = spawnTable[spawnTableIncrement].spawnCount;
                spawnInterval = spawnTable[spawnTableIncrement].spawnInterval;
                enemyIDs = spawnTable[spawnTableIncrement].spawnIDTable;
            }

            // Reset the spawn table timer
            _spawnTableTimer = 0;
        }
    }

    void SpawnTimer()
    {
        _timer += Time.deltaTime;
        _spawnTableTimer += Time.deltaTime;

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
