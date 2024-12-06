using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    [Header("Base Variables")]
    public BaseObjectPool[] objectPools;

    public virtual void Spawn() {}
    public virtual void Spawn(int index, Vector3 spawnPosition) {}
}
