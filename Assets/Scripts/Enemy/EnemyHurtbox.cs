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
        GetComponents();

        if (_damageTextPool != null)
        {
            _damageTextPool.ActivateObject
                (new Color32(255, 0, 0, 255), this.transform.parent, Camera.main.WorldToScreenPoint(this.transform.position), damage);
            _enemyStat.TakeDamage(CalculateDamage(damage));
        }
        else
        {
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D actor)
    {
        ICollectEXP collectEXP = actor.GetComponent<ICollectEXP>();
        if (collectEXP != null)
        {
            _enemyStat.playerStat = actor.GetComponentInParent<PlayerStat>();
        }
    }

    void GetComponents()
    {
        if (_damageTextPool == null)
        {
            Debug.Log("DamageTextPool null. referencing");
            _damageTextPool = FindObjectOfType<DamageTextPool>();
        }

        if (_enemyStat.playerStat == null)
        {

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
