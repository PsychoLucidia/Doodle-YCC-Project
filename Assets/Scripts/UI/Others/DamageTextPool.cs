using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextPool : BaseObjectPool
{
    [Header("Damage Text Pool Parents")]
    [SerializeField] GameObject textParent;
    [SerializeField] GameObject anchorParent;

    [Header("World Space Anchors")]
    public List<GameObject> activeAnchors = new List<GameObject>();
    public List<GameObject> inactiveAnchors = new List<GameObject>();

    [Header("Anchor GameObject")]
    [SerializeField] GameObject anchorObj;

    void Awake()
    {
        Initialization();
    }

    public override void ActivateObject(Color32 color, Transform enemyTransform, Vector2 spawnPosition, int damageAmount)
    {
        if (inactiveObjects.Count > 0)
        {
            GameObject obj = inactiveObjects[0];
            GameObject anchor = inactiveAnchors[0];
            DamageTextHandler handler = obj.GetComponent<DamageTextHandler>();
            handler.damageText.text = damageAmount.ToString();
            handler.color = color;
            handler.positionTransform = inactiveAnchors[0].transform;
            handler.worldSpawnPos = spawnPosition;

            anchor.transform.position = enemyTransform.position;

            obj.SetActive(true);
            handler.PlayAnimation();
            activeObjects.Add(obj);
            inactiveObjects.RemoveAt(0);

            anchor.SetActive(true);
            activeAnchors.Add(anchor);
            inactiveAnchors.RemoveAt(0);

        }
        else
        {
            OnPoolExhausted(color, enemyTransform, spawnPosition, damageAmount);
        }
    }

    void Initialization()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab, transform.position, Quaternion.identity, textParent.transform);
            GameObject anchors = Instantiate(anchorObj, transform.position, Quaternion.identity, anchorParent.transform);
            obj.name = "DamageText" + i;
            anchors.name = "Anchor" + i;
            DamageTextHandler handler = obj.GetComponent<DamageTextHandler>();

            handler.damageTextPool = this;
            handler.damageText.text = "DMG Text";
            handler.color = new Color32(255, 207, 77, 255);
            handler.positionTransform = anchors.transform;

            obj.SetActive(false);
            inactiveObjects.Add(obj);

            anchors.SetActive(false);
            inactiveAnchors.Add(anchors);
        }
    }

    void OnPoolExhausted(Color32 color, Transform enemyTransform, Vector2 spawnPosition, int damageAmount)
    {
        GameObject obj = Instantiate(objectPrefab, transform.position, Quaternion.identity, textParent.transform);
        GameObject anchor = Instantiate(anchorObj, transform.position, Quaternion.identity, anchorParent.transform);
        obj.name = "DamageText" + activeObjects.Count;
        anchor.name = "Anchor" + activeAnchors.Count;

        DamageTextHandler handler = obj.GetComponent<DamageTextHandler>();
        handler.damageTextPool = this;
        handler.damageText.text = damageAmount.ToString();
        handler.color = color;
        handler.positionTransform = anchor.transform;
        handler.worldSpawnPos = spawnPosition;

        anchor.transform.position = enemyTransform.position;

        activeObjects.Add(obj);
        handler.PlayAnimation();
        activeAnchors.Add(anchor);
    }

    public override void DeactivateObject(GameObject textObj, GameObject anchorObj)
    {
        textObj.SetActive(false);
        inactiveObjects.Add(textObj);
        activeObjects.Remove(textObj);

        anchorObj.SetActive(false);
        inactiveAnchors.Add(anchorObj);
        activeAnchors.Remove(anchorObj);
    }
}
