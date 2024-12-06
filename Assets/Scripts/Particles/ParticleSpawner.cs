using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : BaseSpawner
{
    void Awake()
    {
        BaseObjectPool[] pools = GetComponentsInChildren<BaseObjectPool>();

        objectPools = pools;
    }

    public override void Spawn(int index, Vector3 spawnPosition)
    {
        objectPools[index].ActivateObject(spawnPosition);
    }
}
