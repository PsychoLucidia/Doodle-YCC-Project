using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    public bool isAttacking;
    public PlayerAttackState playerAttackState;

    public void PlayerAttack()
    {
        playerAttackState = PlayerAttackState.Attacking;
    }
}

public enum PlayerAttackState
{
    None, Attacking,
}
