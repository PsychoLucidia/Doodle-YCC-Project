using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : BaseHitbox
{
    [SerializeField] private EnemyStat _enemyStat;

    [Header("Hitbox Settings")]
    [SerializeField] private float _hitboxInactiveTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D actor)
    {
        IDamageable damageable = actor.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_enemyStat.damage);
            
        }
    }
}
