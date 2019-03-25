using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    /// <summary>
    /// If the 'Escape' button is pressed and isPaused is false, the Pause() method will be called. 
    /// Otherwise, the Resume() is called.
    /// </summary>
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
		
	}

    /// <summary>
    /// Sets the pauseMenuUI inactive, time scale to 1.0 and isPaused to false.
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    /// <summary>
    /// Sets the pauseMenuUI active, time scale to 0.0 and isPaused to true.
    /// </summary>
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    /// <summary>
    /// Functionality for the buttons in the Pause Menu. When the "Main Menu" button is pressed, 
    /// this Method will be called and will cause the Main Menu to load.
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Functionality for the buttons in the Pause Menu. When the "Quit" button is pressed, 
    /// this Method will be called and will exit the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
