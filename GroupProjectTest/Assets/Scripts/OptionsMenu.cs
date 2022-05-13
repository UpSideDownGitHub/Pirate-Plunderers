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
    public GameObject xpBar;
    public GameObject healthBar;
    public GameObject joystickMove;
    public GameObject joystickAim;
    public GameObject joystickBoat;
    public GameObject joystickCannon;


    public void Active()
    {
        joyStick.SetActive(false);
        xpBar.SetActive(false);
        healthBar.SetActive(false);
        pauseButton.SetActive(false);
        shootButton.SetActive(false);
        miniMap.SetActive(false);
        pauseMenuUI.SetActive(false);
        joystickMove.SetActive(false);
        joystickAim.SetActive(false);
        joystickBoat.SetActive(false);
        joystickCannon.SetActive(false);
        optionsMenu.SetActive(true);
        Time.timeScale = 0f;

    }

    public void NotActive()
    {
        optionsMenu.SetActive(false);
        pauseMenuUI.SetActive(false);
        xpBar.SetActive(true);
        healthBar.SetActive(true);
        pauseButton.SetActive(true);
        shootButton.SetActive(true);
        miniMap.SetActive(true);
        joyStick.SetActive(true);
        joystickMove.SetActive(true);
        joystickAim.SetActive(true);
        joystickBoat.SetActive(true);
        joystickCannon.SetActive(true);
        Time.timeScale = 1f;

    }
}
