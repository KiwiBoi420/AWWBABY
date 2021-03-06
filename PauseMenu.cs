using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //HERE IS THE STARTING POINT
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    // Update is called once per frame




    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsMenuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            if (GameIsPaused)
            {

                Resume();

            }
            else
            {

                Pause();

            }


        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void LoadMenu()
    {

        SceneManager.LoadScene("Main Menu");
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {

        
        Application.Quit();
    }

}
