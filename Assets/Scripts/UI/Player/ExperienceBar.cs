using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBar : BaseBarLogic
{
    [Header("Private Components (Get on Awake)")]
    [SerializeField] PlayerStat _playerStat;

    [Header("Private Variables")]
    [SerializeField] float _currentHealthValue;

    void Awake()
    {
        _playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBar();
    }

    public override void UpdateBar()
    {
        float frontBarAmt = frontBarImage.fillAmount;

        _currentHealthValue = Mathf.InverseLerp(0, _playerStat.expToNextLevel, _playerStat.currentExp);

        if (_currentHealthValue < frontBarAmt)
        {
            LeanTween.cancel(frontBarImage.gameObject);

            LeanTween.value(frontBarImage.gameObject, frontBarAmt, _currentHealthValue, 0.5f)
                .setOnUpdate((float val) => { frontBarImage.fillAmount = val; }).setIgnoreTimeScale(true);
        }

        if (_currentHealthValue > frontBarAmt)
        {
            LeanTween.cancel(frontBarImage.gameObject);

            LeanTween.value(frontBarImage.gameObject, frontBarAmt, _currentHealthValue, 0.5f);
        }
    }
}
