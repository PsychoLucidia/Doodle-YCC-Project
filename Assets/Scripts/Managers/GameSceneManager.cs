using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
    }

    #region Button Functions
    /// <summary>
    /// List of button functions in the game.
    /// </summary>
    public void StartGame(int indexScene)
    {
        SceneManager.LoadSceneAsync(indexScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
