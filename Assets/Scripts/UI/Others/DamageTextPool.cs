using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextPool : BaseObjectPool
{
    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab, transform.position, Quaternion.identity, transform);
            obj.name = "DamageText" + i;
            DamageTextHandler handler = obj.GetComponent<DamageTextHandler>();
            handler.damageText.text = "DMG Text";
            handler.color = new Color32(255, 207, 77, 255);
            obj.SetActive(false);
        }
    }

    public override void ActivateObject()
    {
        if (inactiveObjects.Count > 0)
        {
            GameObject obj = inactiveObjects[0];
            obj.SetActive(true);
            activeObjects.Add(obj);
            inactiveObjects.RemoveAt(0);
        }
    }

    public override void DeactivateObject()
    {
        
    }
}
