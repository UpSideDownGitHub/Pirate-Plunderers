﻿using System.Collections;
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
    public GameObject inventoryButton;
    public GameObject shootButton;


    public void Pause()
    {
        joyStick.SetActive(false);
        pauseButton.SetActive(false);
        inventoryButton.SetActive(false);
        shootButton.SetActive(false);
        miniMap.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
       
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        inventoryButton.SetActive(true);
        shootButton.SetActive(true);
        miniMap.SetActive(true);
        joyStick.SetActive(true);
        Time.timeScale = 1f;
        
    }

    public void LoadMenu()

    {
        Time.timeScale = 1f;
        Debug.Log("loading menu");
        SceneManager.LoadScene(newGameLevel);

    }

        public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
     }
}



