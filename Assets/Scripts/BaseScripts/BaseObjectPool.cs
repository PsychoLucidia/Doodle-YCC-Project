using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjectPool : MonoBehaviour
{
    [Header("Object Pool Settings")]
    public List<GameObject> activeObjects = new List<GameObject>();
    public List<GameObject> inactiveObjects = new List<GameObject>();
    public int poolSize = 10;

    [Header("Object Prefab")]
    public GameObject objectPrefab;

    public virtual void ActivateObject(Color32 color) {}
    public virtual void ActivateObject(Color32 color, Transform transform, Vector2 spawnPosition, int damageAmount) {}
    public virtual void ActivateObject(int level, int speed, int damage) {}

    public virtual void DeactivateObject(GameObject textObj, GameObject anchorObj) {}
}
