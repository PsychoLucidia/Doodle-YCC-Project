using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int initialSpawnCount = 1;
    public float initialInterval = 4f;
    public float spawnOffset = 1f;

    [SerializeField] int spawnCount;
    [SerializeField] float spawnInterval;

    public SpawnTable spawnTable;
    SpawnTable _spawnTable;

    public int[] enemyIDs;

    float _timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = initialSpawnCount;
        spawnInterval = initialInterval;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            case 2: spawnPos = new Vector3(screenMax.x - spawnOffset, Random.Range(screenMin.y, screenMax.y), 0); break; // Left
            case 3: spawnPos = new Vector3(screenMin.x + spawnOffset, Random.Range(screenMin.y, screenMax.y), 0); break; // Right
            default: break; // Error
        }

        
    }

    void SpawnTimer()
    {
        _timer += Time.deltaTime;

        if (_timer > spawnInterval)
        {

            _timer = 0f;
        }
    }

    void SpawnTableManager()
    {
        if (_spawnTable != spawnTable)
        {
            _spawnTable = spawnTable;

            enemyIDs = spawnTable.spawnIDTable;
        }
    }
}

[CreateAssetMenu(fileName = "SpawnTable", menuName = "Spawners/Spawn Table", order = 1)]
public class SpawnTable : ScriptableObject
{
    public int[] spawnIDTable;
}
