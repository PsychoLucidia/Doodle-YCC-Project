using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHitbox : MonoBehaviour
{
    public int currentAttackDamage;

    /*
    public void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(currentAttackDamage);
        }
    }
    */
}

public interface IDamageable
{
    void TakeDamage(int damage);
}
