using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public string newGameLevel;
    public GameObject pauseMenuUI;
    public GameObject joyStick;
    public GameObject miniMap;
    public GameObject pauseButton;
    public GameObject shootButton;
    public GameObject xpBar;
    public GameObject healthBar;


    public void Pause()
    {
        joyStick.SetActive(false);
        xpBar.SetActive(false);
        healthBar.SetActive(false);
        pauseButton.SetActive(false);
        shootButton.SetActive(false);
        miniMap.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
       
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        xpBar.SetActive(true);
        healthBar.SetActive(true);
        shootButton.SetActive(true);
        miniMap.SetActive(true);
        joyStick.SetActive(true);
        Time.timeScale = 1f;
        
    }

    public void LoadMenu()

    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
     }
}



