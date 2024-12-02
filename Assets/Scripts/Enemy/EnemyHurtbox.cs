using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : BaseHurtbox, IDamageable
{
    [SerializeField] EnemyStat _enemyStat;
    DamageTextPool _damageTextPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (_damageTextPool == null)
        {
            Debug.Log("DamageTextPool null. referencing");
            _damageTextPool = FindObjectOfType<DamageTextPool>();
        }

        if (_damageTextPool != null)
        {
            _damageTextPool.ActivateObject
                (new Color32(255, 0, 0, 255), this.transform.parent, Camera.main.WorldToScreenPoint(this.transform.position), damage);
            _enemyStat.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    protected override int CalculateDamage(int inputDamage)
    {
        int initialDamage = inputDamage;

        if (_enemyStat != null)
        {

        }
        return inputDamage;
    }
}
