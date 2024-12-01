using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObjectPool : MonoBehaviour
{
    [Header("Object Pool Settings")]
    public List<GameObject> activeObjects = new List<GameObject>();
    public List<GameObject> inactiveObjects = new List<GameObject>();
    public int poolSize = 10;

    [Header("Object Prefab")]
    public GameObject objectPrefab;

    public virtual void ActivateObject() {}
    public virtual void DeactivateObject() {}
}
