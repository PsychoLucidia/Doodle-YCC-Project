using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifebar : BaseBarLogic
{
    [Header("Private Components (Get on Awake)")]
    [SerializeField] PlayerStat _playerStat;

    [Header("Private Variables")]
    [SerializeField] float currentHealthValue;

    void Awake()
    {
        _playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
    }

    void Update()
    {
        UpdateBar();
    }

    public override void UpdateBar()
    {
        float frontBarAmt = frontBarImage.fillAmount;
        float backBarAmt = backBarImage.fillAmount;
        currentHealthValue = Mathf.InverseLerp(0, _playerStat.maxHealth, _playerStat.health);

        if (currentHealthValue < frontBarAmt)
        {
            LeanTween.cancel(frontBarImage.gameObject);
            LeanTween.cancel(backBarImage.gameObject);

            frontBarImage.fillAmount = currentHealthValue;
            backBarImage.color = Color.red;

            LeanTween.value(backBarImage.gameObject, backBarAmt, currentHealthValue, 0.5f).setOnUpdate((float val) => 
                { backBarImage.fillAmount = val; }).setIgnoreTimeScale(true);
        }

        if (currentHealthValue > backBarAmt)
        {
            LeanTween.cancel(frontBarImage.gameObject);
            LeanTween.cancel(backBarImage.gameObject);

            backBarImage.fillAmount = currentHealthValue;
            backBarImage.color = Color.cyan;

            LeanTween.value(frontBarImage.gameObject, frontBarAmt, currentHealthValue, 0.5f).setOnUpdate((float val) => 
                { frontBarImage.fillAmount = val; }).setIgnoreTimeScale(true);{}
        }
    }
}
