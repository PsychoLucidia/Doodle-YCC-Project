using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyPool : BaseObjectPool
{
    void Awake()
    {
        Initialization();
    }

    public override void ActivateObject(int level, int speed, int damage)
    {
        
    }
    
    void Initialization()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(objectPrefab, transform.position, Quaternion.identity, transform);

            obj.name = "SlimeEnemy" + i;

            obj.SetActive(false);
            inactiveObjects.Add(obj);
        }
    }
}
