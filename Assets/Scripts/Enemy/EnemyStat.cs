using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : BaseStat
{
    [Header("EXP Drop")]
    public int xpDrop;

    public PlayerStat playerStat;
    public EnemyPool enemyPool;
    public ParticleSpawner particleSpawner;
    public int particleID;
    [SerializeField] private EnemyMovement _enemyMovement;

    void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetSpeed();
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        playerStat.currentExp += xpDrop;
        particleSpawner.Spawn(particleID, this.transform.position);
        enemyPool.DeactivateObject(this.gameObject);
    }

    void StatCalculation()
    {
        
    }

    void SetSpeed()
    {
        if (_enemyMovement != null)
        {
            if (_enemyMovement.entitySpeed != speed)
            {
                _enemyMovement.entitySpeed = speed;
            }
        }
    }
}
