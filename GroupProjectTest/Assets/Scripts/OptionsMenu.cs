using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public string newGameLevel;
    public GameObject pauseMenuUI;
    public GameObject joyStick;
    public GameObject miniMap;
    public GameObject pauseButton;
    public GameObject shootButton;
    public GameObject optionsMenu;


    public void Active()
    {
        joyStick.SetActive(false);
        pauseButton.SetActive(false);
        shootButton.SetActive(false);
        miniMap.SetActive(false);
        pauseMenuUI.SetActive(false);
        optionsMenu.SetActive(true);
        Time.timeScale = 0f;

    }

    public void NotActive()
    {
        optionsMenu.SetActive(false);
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        shootButton.SetActive(true);
        miniMap.SetActive(true);
        joyStick.SetActive(true);
        Time.timeScale = 1f;

    }
}
