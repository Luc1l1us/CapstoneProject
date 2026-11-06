using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject resumeButton;
    public GameObject pauseButton;
    public GameObject gameCanvas;
    public CanvasGroup[] uiElements;
    public static bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);

        foreach (CanvasGroup uiElements in uiElements) {
            uiElements.alpha = 1f;
            uiElements.blocksRaycasts = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();

        }
        else
        {
            PauseGame();
        }

         foreach (CanvasGroup uiElements in uiElements) {
            uiElements.alpha = isPaused ? 1f : 0f;
            uiElements.blocksRaycasts = isPaused;
            Time.timeScale = isPaused ? 1 : 0;
        } 
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        gameCanvas.SetActive(true); 
        Time.timeScale = 1;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        gameCanvas.SetActive(false); 
        Time.timeScale = 0;
        isPaused = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
