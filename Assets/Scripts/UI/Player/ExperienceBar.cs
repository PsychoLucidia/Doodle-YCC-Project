using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceBar : BaseBarLogic
{
    [Header("Private Components (Get on Awake)")]
    [SerializeField] PlayerStat _playerStat;
    [SerializeField] TextMeshProUGUI _levelText;

    [Header("Private Variables")]
    float _currentHealthValue;
    float _previousHealthValue;
    float _currentLevelValue;
    float _previousLevelValue;

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
        LevelText();
    }

    /// <summary>
    /// Updates the experience bar based on the player's current experience.
    /// The bar is divided into two images: the front bar and the back bar.
    /// The front bar is for the player's current experience, while the back bar is for the player's total experience to the next level.
    /// When the player's experience changes, the respective bar is updated with a LeanTween animation.
    /// </summary>
    public override void UpdateBar()
    {
        // Get the current fill amounts of the front and back bars
        float frontBarAmt = frontBarImage.fillAmount;

        // Calculate the fill amount of the experience bar based on the player's current experience
        _currentHealthValue = Mathf.InverseLerp(0, _playerStat.expToNextLevel, _playerStat.currentExp);

        // If the player's experience has changed
        if (_currentHealthValue != _previousHealthValue)
        {
            // Store the new value of the front bar
            _previousHealthValue = _currentHealthValue;

            // Cancel any existing animations
            LeanTween.cancel(frontBarImage.gameObject);

            // Animate the front bar to the new value
            LeanTween.value(frontBarImage.gameObject, frontBarAmt, _currentHealthValue, 0.5f).setEaseOutCirc()
                .setOnUpdate((float val) => { frontBarImage.fillAmount = val; }).setIgnoreTimeScale(true);
        }
    }

    void LevelText()
    {
        _currentLevelValue = _playerStat.level;
        
        if (_currentLevelValue != _previousLevelValue)
        {
            _levelText.text = _playerStat.level.ToString();
        }
    }
}
