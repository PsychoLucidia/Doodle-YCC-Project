using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteParticlePool : BaseObjectPool
{
    void Awake()
    {
        Initialization();
    }

    public override void ActivateObject(Vector3 spawnPosition)
    {   
        if (inactiveObjects.Count > 0)
        {
            GameObject obj = inactiveObjects[0];
            SpriteParticleHandler handler = obj.GetComponent<SpriteParticleHandler>();

            obj.SetActive(true);
            obj.transform.position = spawnPosition;
            handler.PlayAnimation();


            activeObjects.Add(obj);
            inactiveObjects.RemoveAt(0);
        }
        else
        {
            if (!limitToPoolSize)
            {
                GameObject obj = Instantiate(objectPrefab, spawnPosition, Quaternion.identity, transform);
                SpriteParticleHandler handler = obj.GetComponent<SpriteParticleHandler>();

                obj.name = gameObject.name + activeObjects.Count;

                obj.transform.position = spawnPosition;
                handler.spriteParticlePool = this;
                handler.PlayAnimation();

                activeObjects.Add(obj);
            }
        }
    }

    void Initialization()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab, transform.position, Quaternion.identity, transform);
            SpriteParticleHandler handler = obj.GetComponent<SpriteParticleHandler>();

            obj.name = gameObject.name + i;

            handler.spriteParticlePool = this;

            obj.SetActive(false);
            inactiveObjects.Add(obj);
        }
    }
}
