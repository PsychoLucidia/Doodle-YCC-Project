using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    void Update()
    {
        _timerText.text = ConvertToTimeFormat(GameManager.Instance.totalPlayTime);
    }

    string ConvertToTimeFormat(float time)
    {
        int minuteText = Mathf.FloorToInt(time / 60);
        int secondsText = Mathf.FloorToInt(time % 60);

        string timeText = minuteText.ToString("00") + ":" + secondsText.ToString("00");

        return timeText;
    }
}
