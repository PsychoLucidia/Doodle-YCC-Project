using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSelector : MonoBehaviour
{
    [Header("Settings")]
    public int selectableCount = 3;

    [Header("Prefabs")]
    public GameObject buffPanelPrefab;

    [Header("Buff Selector UI")]
    [SerializeField] private GameObject _buffUICanvas;


    void Awake()
    {
        _buffUICanvas = GameObject.FindGameObjectWithTag("BuffUI");
    }

    // Start is called before the first frame update
    void Start()
    {
        _buffUICanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum BuffType
{
    None, Health, Speed, Attack, RestoreHealth, 
}

[CreateAssetMenu(fileName = "BuffData", menuName = "ScriptableObjects/BuffData")]
public class BuffData : ScriptableObject
{
    public BuffType buffType;
    public int health;
    public int attack;
    public int restoreHealthAmount;
    public float speed;
}