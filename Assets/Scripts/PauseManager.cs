using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool pauseOn;

    void Start()
    {
        pauseOn = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseResumeGame();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoMenu();
        }
        if (!PauseManager.pauseOn)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        pauseOn = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseResumeGame()
    {
        if (PauseManager.pauseOn)
        {
            PauseManager.pauseOn = false;
            Time.timeScale = 1;
        }
        else
        {
            PauseManager.pauseOn = true;
            Time.timeScale = 0;
        }
    }

    public void GoMenu()
    {
        pauseOn = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
