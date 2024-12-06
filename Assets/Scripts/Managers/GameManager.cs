using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnDifficultyLevelChanged; 

    public static GameManager Instance;

    [Header("Enums")]
    public PauseState pauseState;
    private PauseState _previousPauseState;

    [Header("Stats")]
    public PlayerStat playerStat;

    public float playTime;
    public float totalPlayTime;
    
    public int difficultyLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Initialization();
    }

    void Initialization()
    {
        playerStat = FindObjectOfType<PlayerStat>();
    }

    void Update()
    {
        IncrementPlayTime();
    }

    void IncrementPlayTime()
    {
        if (pauseState == PauseState.Unpaused)
        {
            playTime += Time.deltaTime;
            totalPlayTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// Toggles the game's pause state.
    /// </summary>
    /// <param name="pauseState">The desired pause state (Paused or Unpaused).</param>
    /// <remarks>
    /// If the new pause state is the same as the current one, the method returns immediately.
    /// If the state is set to Paused, the game's time scale is set to 0, effectively pausing the game.
    /// Otherwise, the time scale is set to 1, resuming normal game flow.
    /// </remarks>
    public void PauseGame(PauseState pauseState)
    {
        if (_previousPauseState == pauseState) { return; }

        _previousPauseState = pauseState;
        
        if (pauseState == PauseState.Paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}

public enum PauseState
{
    Unpaused, Paused
}
