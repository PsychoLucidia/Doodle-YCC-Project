using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSelector : MonoBehaviour
{
    [Header("Settings")]
    public int selectableCount = 3;

    [Header("Prefabs")]
    public GameObject buffPanelPrefab;

    [Header("Lists")]
    public BuffData[] buffCommon;
    public BuffData[] buffUncommon;
    public BuffData[] buffRare;

    [Header("Buff Selector UI")]
    [SerializeField] private GameObject _buffUICanvas;
    [SerializeField] private GameObject _buffUI;

    public List<GameObject> buffPanelList = new List<GameObject>();

    void Awake()
    {
        Initialization();
        ObjectsInitialization();
    }

    void Initialization()
    {

        buffCommon = Resources.LoadAll<BuffData>("Buffs/Common");
        buffUncommon = Resources.LoadAll<BuffData>("Buffs/Uncommon");
        buffRare = Resources.LoadAll<BuffData>("Buffs/Rare");
    }

    void ObjectsInitialization()
    {
        _buffUICanvas = GameObject.FindGameObjectWithTag("BuffUI");

        _buffUI = _buffUICanvas.transform.Find("BuffUI").gameObject;

        Transform buffPanel = _buffUI.transform.Find("BuffPanel");

        for (int i = 0; i < selectableCount; i++)
        {
            GameObject buffPanelObj = Instantiate(buffPanelPrefab, buffPanel.transform.position, Quaternion.identity, buffPanel.transform);

            buffPanelList.Add(buffPanelObj);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        _buffUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OpenBuffPanel()
    {

    }
}

public enum BuffType
{
    None, Health, Speed, Attack, RestoreHealth, 
}
