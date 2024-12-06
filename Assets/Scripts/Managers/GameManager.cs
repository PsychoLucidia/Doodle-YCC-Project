using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnDifficultyLevelChanged; 
    public UnityEvent UnityEventDiffChange;

    public static GameManager Instance;
    public GameObject pauseMenu;

    [Header("Enums")]
    public PauseState pauseState;
    private PauseState _previousPauseState;

    [Header("Spawners")]
    public EnemySpawner enemySpawner;

    [Header("Stats")]
    public PlayerStat playerStat;

    public float playTime;
    public float totalPlayTime;

    public bool buffPanelOpen;
    
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

        Time.timeScale = 1f;

        Initialization();
    }

    void Initialization()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        enemySpawner = FindObjectOfType<EnemySpawner>();

        pauseMenu = GameObject.FindGameObjectWithTag("PausePanel");
        pauseMenu.SetActive(false);
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

        if (pauseState == PauseState.Paused)
        {
            Time.timeScale = 0f;
            
            if (!buffPanelOpen)
            {
                pauseMenu.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
        
        this.pauseState = pauseState;
        _previousPauseState = this.pauseState;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseState = PauseState.Unpaused;
    }

    public void ChangeDifficultyLevel()
    {
        if (playTime > 300f)
        {
            playTime = 0f;            
            difficultyLevel++;
            OnDifficultyLevelChanged?.Invoke(difficultyLevel);
        }
    }
}

public enum PauseState
{
    Unpaused, Paused
}
