using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHurtbox : MonoBehaviour
{
    protected abstract int CalculateDamage(int inputDamage);
}
