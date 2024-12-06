using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSelector : MonoBehaviour
{
    [Header("Components")]
    public PlayerStat playerStat;

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
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();

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
            BuffPanelHandler handler = buffPanelObj.GetComponent<BuffPanelHandler>();
            buffPanelObj.name = "BuffPanel" + (i + 1);

            handler.playerStat = this.playerStat;

            buffPanelList.Add(buffPanelObj);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        _buffUI.SetActive(false);
    }

    void OnEnable()
    {
        if (playerStat == null) { playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>(); }

        if (playerStat != null)
        {
            playerStat.OnLevelUp += OpenBuffPanel;
        }
    }

    void OnDisable()
    {
        if (playerStat != null)
        {
            playerStat.OnLevelUp -= OpenBuffPanel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OpenBuffPanel(PauseState pauseState)
    {
        GameManager.Instance.PauseGame(pauseState);
        _buffUI.SetActive(true);
        for (int i = 0; i < selectableCount; i++)
        {
            BuffData buffData = GetBuffType();
            BuffPanelHandler handler = buffPanelList[i].GetComponent<BuffPanelHandler>();
            handler.buffData = buffData;
            handler.buffName.text = buffData.buffName;
            handler.buffDescription.text = buffData.description;
            handler.buffImage.color = buffData.buffColor;

            handler.applyBuffButton.onClick.RemoveAllListeners();
            handler.applyBuffButton.onClick.AddListener(handler.ApplyBuff);
            handler.applyBuffButton.onClick.AddListener(this.DeactivateBuffUI);
        }
    }

    BuffData GetBuffType()
    {
        float chance = Random.Range(0f, 1f);

        if (chance <= 0.5f) { return buffCommon[Random.Range(0, buffCommon.Length)]; }
        else if (chance > 0.5f && chance <= 0.8f) { return buffUncommon[Random.Range(0, buffUncommon.Length)]; }
        else if (chance > 0.8f) { return buffRare[Random.Range(0, buffRare.Length)]; }
        return null;
    }

    public void DeactivateBuffUI()
    {
        _buffUI.SetActive(false);
        GameManager.Instance.PauseGame(PauseState.Unpaused);
    }
}

public enum BuffType
{
    None, Health, Speed, Attack, RestoreHealth, Combined
}
