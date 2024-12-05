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
    [SerializeField] EnemyMovement _enemyMovement;

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
        enemyPool.DeactivateObject(this.gameObject);
    }

    void StatCalculation()
    {
        
    }
}
