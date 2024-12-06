using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : BaseHitbox
{
    [SerializeField] private PlayerAttackHandler _playerAttackHandler;
    [SerializeField] private Animator _weaponAnimator;
    private PlayerMovementCC _playerMovementCC;
    

    Coroutine attackCoroutine;

    void OnEnable()
    {
        _playerAttackHandler.OnSendAttackDamage += SetCurrentDamage;
        _playerAttackHandler.OnPlayerAttack += PlayAnimation;

        if (_playerMovementCC == null)
        {
            _playerMovementCC = GetComponentInParent<PlayerMovementCC>();
        }

        if (_playerMovementCC != null) { _playerMovementCC.OnPlayerRawRotationChange += CurrentRotation; }
    }

    void OnDisable()
    {
        _playerAttackHandler.OnSendAttackDamage -= SetCurrentDamage;
        _playerAttackHandler.OnPlayerAttack -= PlayAnimation;

        _playerMovementCC.OnPlayerRawRotationChange -= CurrentRotation;
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
        
        ICollectEXP setPlayerStat = other.GetComponent<ICollectEXP>();
        if (setPlayerStat != null)
        {
            setPlayerStat.SetPlayerStat(this.transform.GetComponentInParent<PlayerStat>());
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
