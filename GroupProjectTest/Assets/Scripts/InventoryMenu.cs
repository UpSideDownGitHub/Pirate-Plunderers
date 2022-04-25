using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryMenu : MonoBehaviour
{


    public GameObject joyStick;
    public GameObject shootButton;
    public GameObject miniMap;
    public GameObject pauseButton;
    public GameObject inventoryButton;
    public string newGameLevel;
    public GameObject inventoryMenuUI;


    // Update is called once per frame

    public void Resume()
    {
        joyStick.SetActive(true);
        shootButton.SetActive(true);
        miniMap.SetActive(true);
        pauseButton.SetActive(true);
        inventoryButton.SetActive(true);
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 1f;
        
    }

    public void Pause()
    {
        miniMap.SetActive(false);
        shootButton.SetActive(false);
        joyStick.SetActive(false);
        pauseButton.SetActive(false);
        inventoryButton.SetActive(false);
        inventoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
        

    }


   
}




