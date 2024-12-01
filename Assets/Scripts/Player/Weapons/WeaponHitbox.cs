using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : BaseHitbox
{
    [SerializeField] PlayerAttackHandler _playerAttackHandler;
    [SerializeField] Animator _weaponAnimator;

    Coroutine attackCoroutine;

    void OnEnable()
    {
        PlayerAttackHandler.OnSendAttackDamage += SetCurrentDamage;
        PlayerAttackHandler.OnPlayerAttack += PlayAnimation;
        PlayerMovementCC.OnPlayerRawRotationChange += CurrentRotation;
    }

    void OnDisable()
    {
        PlayerAttackHandler.OnSendAttackDamage -= SetCurrentDamage;
        PlayerMovementCC.OnPlayerRawRotationChange -= CurrentRotation;
    }


    public void SetCurrentDamage(int damage)
    {
        currentAttackDamage = damage;
    }

    public void CurrentRotation(float rotation)
    {
        if(_playerAttackHandler.playerAttackState != PlayerAttackState.Attacking)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }

    void PlayAnimation()
    {
        if (!PlayerAttackHandler.isAttacking)
        {
            attackCoroutine = StartCoroutine(AttackSequence());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.gameObject.name);
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(currentAttackDamage);
        }
    }

    IEnumerator AttackSequence()
    {
        _weaponAnimator.Play("MainSwordSwing");

        while (!_weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("MainSwordSwing")) { yield return null; }

        yield return new WaitForSeconds(_weaponAnimator.GetCurrentAnimatorStateInfo(0).length);
        _weaponAnimator.Play("MainIdle");
        attackCoroutine = null;
    }
}
