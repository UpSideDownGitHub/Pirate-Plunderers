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
    public GameObject joystickMove;
    public GameObject joystickAim;
    public GameObject joystickBoat;
    public GameObject joystickCannon;


    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        joyStick.SetActive(false);
        xpBar.SetActive(false);
        healthBar.SetActive(false);
        pauseButton.SetActive(false);
        shootButton.SetActive(false);
        miniMap.SetActive(false);
        miniMap.SetActive(false);
        joystickMove.SetActive(false);
        joystickAim.SetActive(false);
        joystickBoat.SetActive(false);
        joystickCannon.SetActive(false);
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
        joystickMove.SetActive(true);
        joystickAim.SetActive(true);
        joystickBoat.SetActive(true);
        joystickCannon.SetActive(true);
        Time.timeScale = 1f;

    }

    public void LoadMenu()

    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);

    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }
}



