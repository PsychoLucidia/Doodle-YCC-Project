using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : BaseHurtbox, IDamageable, ICollectEXP
{
    public EnemyStat enemyStat;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    DamageTextPool _damageTextPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Takes damage and displays a damage text object if available.
    /// </summary>
    /// <param name="damage">The amount of damage to take.</param>
    public void TakeDamage(int damage)
    {
        GetComponents();

        if (_damageTextPool != null)
        {
            _damageTextPool.ActivateObject
                (new Color32(255, 0, 0, 255), this.transform.parent, Camera.main.WorldToScreenPoint(this.transform.position), damage);
            if (enemyStat != null)
            {
                enemyStat.TakeDamage(CalculateDamage(damage));
                if (enemyStat.health > 0)
                {
                    StartCoroutine(AnimateHurt());
                }
            }
        }
        else
        {
            return;
        }
    }

    void GetComponents()
    {
        if (_damageTextPool == null)
        {
            Debug.Log("DamageTextPool null. referencing");
            _damageTextPool = FindObjectOfType<DamageTextPool>();
        }
    }

    protected override int CalculateDamage(int inputDamage)
    {
        int initialDamage = inputDamage;

        if (enemyStat != null)
        {

        }
        return inputDamage;
    }

    public void SetPlayerStat(PlayerStat thisPlayerStat)
    {
        enemyStat.playerStat = thisPlayerStat;
    }

    IEnumerator AnimateHurt()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        _spriteRenderer.color = Color.white;
    }
}
