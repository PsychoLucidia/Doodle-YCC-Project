using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifebar : BaseBarLogic
{
    [Header("Private Components (Get on Awake)")]
    [SerializeField] PlayerStat _playerStat;

    [Header("Private Variables")]
    [SerializeField] float _currentHealthValue;

    void Awake()
    {
        _playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
    }

    void Update()
    {
        UpdateBar();
    }

    /// <summary>
    /// Updates the health bar based on the player's current health.
    /// The health bar is divided into two images: the front bar and the back bar.
    /// The front bar is for the player's current health, while the back bar is for the player's maximum health.
    /// When the player's health changes, the respective bar is updated with a LeanTween animation.
    /// </summary>
    public override void UpdateBar()
    {
        // Get the current fill amounts of the front and back bars
        float frontBarAmt = frontBarImage.fillAmount;
        float backBarAmt = backBarImage.fillAmount;

        // Calculate the fill amount of the health bar based on the player's current health
        _currentHealthValue = Mathf.InverseLerp(0, _playerStat.maxHealth, _playerStat.health);

        // If the player's health has decreased
        if (_currentHealthValue < frontBarAmt)
        {
            // Cancel any existing animations
            LeanTween.cancel(frontBarImage.gameObject);
            LeanTween.cancel(backBarImage.gameObject);

            // Set the front bar to the current health value
            frontBarImage.fillAmount = _currentHealthValue;

            // Set the back bar's color to red
            backBarImage.color = Color.red;

            // Animate the back bar to the current health value
            LeanTween.value(backBarImage.gameObject, backBarAmt, _currentHealthValue, 0.5f).setOnUpdate((float val) => 
                { backBarImage.fillAmount = val; }).setIgnoreTimeScale(true);
        }

        // If the player's health has increased
        if (_currentHealthValue > backBarAmt)
        {
            // Cancel any existing animations
            LeanTween.cancel(frontBarImage.gameObject);
            LeanTween.cancel(backBarImage.gameObject);

            // Set the back bar to the current health value
            backBarImage.fillAmount = _currentHealthValue;

            // Set the back bar's color to cyan
            backBarImage.color = Color.cyan;

            // Animate the front bar to the current health value
            LeanTween.value(frontBarImage.gameObject, frontBarAmt, _currentHealthValue, 0.5f).setOnUpdate((float val) => 
                { frontBarImage.fillAmount = val; }).setIgnoreTimeScale(true);
        }
    }
}
