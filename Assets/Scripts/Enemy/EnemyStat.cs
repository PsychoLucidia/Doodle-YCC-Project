using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStat : BaseStat
{
    public UnityEvent OnEnemyHurt;
    public UnityEvent OnEnemyDie;

    [Header("EXP Drop")]
    public int xpDrop;

    public PlayerStat playerStat;
    public EnemyPool enemyPool;
    public ParticleSpawner particleSpawner;
    public int particleID;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private float _despawnTime;


    void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        Transform transformSFX = soundManager.transform.Find("SFX");
        AudioSource audioSource = transformSFX.transform.Find("EnemyDie").GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null");
            return;
        }
        else Debug.Log("AudioSource is not null");
        OnEnemyDie.AddListener(audioSource.Play);
    }

    // Update is called once per frame
    void Update()
    {
        SetSpeed();
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;

        OnEnemyHurt?.Invoke();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnEnemyDie?.Invoke();
        playerStat.currentExp += xpDrop;
        particleSpawner.Spawn(particleID, this.transform.position);
        HandleDespawn();
    }

    void HandleDespawn()
    {
        StartCoroutine(DespawnTimer(_despawnTime));
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

    IEnumerator DespawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        enemyPool.DeactivateObject(this.gameObject);
    }
}
