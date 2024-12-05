using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjectPool : MonoBehaviour
{
    [Header("Object Pool Settings")]
    public List<GameObject> activeObjects = new List<GameObject>();
    public List<GameObject> inactiveObjects = new List<GameObject>();
    public int poolSize = 10;
    public bool limitToPoolSize = false;

    [Header("Object Prefab")]
    public GameObject objectPrefab;

    /// <summary>
    /// A overload method that activates a damage text object and its corresponding
    /// anchor with the specified parameters.
    /// </summary>
    /// <param name="color">Specifies the color of the damage text.</param>
    /// <param name="transform">Specifies the transform of the enemy to position the anchor.</param>
    /// <param name="spawnPosition">Specifies the position to spawn the text in screen space.</param>
    /// <param name="damageAmount"></param>
    public virtual void ActivateObject(Color32 color, Transform anchorTransform, Vector2 spawnPosition, int damageAmount)
    {
    }

    /// <summary>
    /// Activates an enemy object at the specified level and position.
    /// </summary>
    /// <param name="level">The level of the object to activate.</param>
    /// <param name="spawnPosition">The position to spawn the object in world space.</param>
    public virtual void ActivateObject(int level, Vector3 spawnPosition)
    {
    }

    public virtual void DeactivateObject(GameObject textObj, GameObject anchorObj)
    {
    }

    public virtual void DeactivateObject(GameObject enemyObj) 
    {
        enemyObj.SetActive(false);
        inactiveObjects.Add(enemyObj);
    }
    
    protected virtual void OnPoolExhausted(Color32 color, Transform enemyTransform, Vector2 spawnPosition, int damageAmount)
    {
    }

    protected virtual void OnPoolExhausted(int level, Vector3 spawnPosition)
    {
    }
}
