using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PauseState pauseState;

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
}

public enum PauseState
{
    Unpaused, Paused
}
