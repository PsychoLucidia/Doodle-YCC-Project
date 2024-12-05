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

    /// <summary>
    /// Activates a damage text object and its corresponding anchor with the specified parameters.
    /// If there are no inactive objects, creates new ones.
    /// </summary>
    /// <param name="color">The color of the damage text.</param>
    /// <param name="enemyTransform">The transform of the enemy to position the anchor.</param>
    /// <param name="spawnPosition">The position to spawn the text in screen space.</param>
    /// <param name="damageAmount">The amount of damage to display.</param>
    public override void ActivateObject(Color32 color, Transform enemyTransform, Vector2 spawnPosition, int damageAmount)
    {
        if (inactiveObjects.Count > 0)
        {
            // Retrieve the first inactive object and anchor
            GameObject obj = inactiveObjects[0];
            GameObject anchor = inactiveAnchors[0];

            // Configure the damage text handler
            DamageTextHandler handler = obj.GetComponent<DamageTextHandler>();
            handler.damageText.text = damageAmount.ToString();
            handler.color = color;
            handler.positionTransform = anchor.transform;
            handler.worldSpawnPos = spawnPosition;

            // Set the anchor position to the enemy's position
            anchor.transform.position = enemyTransform.position;

            // Activate the object and its anchor, then move them to active lists
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
            // If no inactive objects are available, handle pool exhaustion
            OnPoolExhausted(color, enemyTransform, spawnPosition, damageAmount);
        }
    }

    /// <summary>
    /// Initializes the damage text pool by creating the specified number of objects.
    /// </summary>
    void Initialization()
    {
        // Create the specified number of damage text objects and anchors
        for (int i = 0; i < poolSize; i++)
        {
            // Instantiate the damage text prefab
            GameObject obj = Instantiate(objectPrefab, transform.position, Quaternion.identity, textParent.transform);
            // Set the name of the object to "DamageText" + the index
            obj.name = "DamageText" + i;

            // Instantiate the anchor prefab
            GameObject anchors = Instantiate(anchorObj, transform.position, Quaternion.identity, anchorParent.transform);
            // Set the name of the anchor to "Anchor" + the index
            anchors.name = "Anchor" + i;

            // Get the DamageTextHandler component on the object
            DamageTextHandler handler = obj.GetComponent<DamageTextHandler>();

            // Set the damage text pool on the handler
            handler.damageTextPool = this;
            // Set the text of the damage text handler to "DMG Text"
            handler.damageText.text = "DMG Text";
            // Set the color of the damage text handler to yellow
            handler.color = new Color32(255, 207, 77, 255);
            // Set the position transform of the damage text handler to the anchor transform
            handler.positionTransform = anchors.transform;

            // Deactivate the damage text object
            obj.SetActive(false);
            // Add the object to the inactive objects list
            inactiveObjects.Add(obj);

            // Deactivate the anchor object
            anchors.SetActive(false);
            // Add the anchor to the inactive anchors list
            inactiveAnchors.Add(anchors);
        }
    }

    /// <summary>
    /// Handles the situation when the damage text pool is exhausted.
    /// Instantiates new damage text and anchor objects with specified parameters.
    /// </summary>
    /// <param name="color">The color of the damage text.</param>
    /// <param name="enemyTransform">The transform of the enemy to position the anchor.</param>
    /// <param name="spawnPosition">The position to spawn the text in screen space.</param>
    /// <param name="damageAmount">The amount of damage to display.</param>
    protected override void OnPoolExhausted(Color32 color, Transform enemyTransform, Vector2 spawnPosition, int damageAmount)
    {
        // Instantiate new damage text and anchor objects
        GameObject obj = Instantiate(objectPrefab, transform.position, Quaternion.identity, textParent.transform);
        GameObject anchor = Instantiate(anchorObj, transform.position, Quaternion.identity, anchorParent.transform);
        
        // Set unique names for the new objects
        obj.name = "DamageText" + activeObjects.Count;
        anchor.name = "Anchor" + activeAnchors.Count;

        // Configure the damage text handler
        DamageTextHandler handler = obj.GetComponent<DamageTextHandler>();
        handler.damageTextPool = this;
        handler.damageText.text = damageAmount.ToString();
        handler.color = color;
        handler.positionTransform = anchor.transform;
        handler.worldSpawnPos = spawnPosition;

        // Position the anchor at the enemy's location
        anchor.transform.position = enemyTransform.position;

        // Add to active lists and play animation
        activeObjects.Add(obj);
        handler.PlayAnimation();
        activeAnchors.Add(anchor);
    }

    /// <summary>
    /// Deactivates the given damage text object and its corresponding anchor.
    /// Adds the deactivated objects to the inactive lists and removes them from active lists.
    /// </summary>
    /// <param name="textObj">The damage text object to deactivate.</param>
    /// <param name="anchorObj">The anchor object to deactivate.</param>
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
