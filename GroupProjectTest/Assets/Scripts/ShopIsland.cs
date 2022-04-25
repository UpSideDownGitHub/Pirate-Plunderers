using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopIsland : MonoBehaviour
{


    public static bool GameIsPaused = false;
    public string newGameLevel;
    public GameObject shopActiveButton;
    public GameObject shopMenuUI;
     public GameObject joyStick;
    public GameObject shootButton;
    public GameObject miniMap;
    public GameObject pauseButton;
    public GameObject inventoryButton;


    // Update is called once per frame
    void OnTriggerEnter2D (Collider2D collision)
    {
        shopActiveButton.SetActive(true);          
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        shopActiveButton.SetActive(false);
    }

    public void ShopEnter()
    {
        miniMap.SetActive(false);
        shootButton.SetActive(false);
        joyStick.SetActive(false);
        pauseButton.SetActive(false);
        inventoryButton.SetActive(false);
        shopActiveButton.SetActive(false);
        shopMenuUI.SetActive(true);
    }

    public void ShopExit()
    {
        miniMap.SetActive(true);
        shootButton.SetActive(true);
        joyStick.SetActive(true);
        pauseButton.SetActive(true);
        inventoryButton.SetActive(true);
        shopActiveButton.SetActive(true);
        shopMenuUI.SetActive(false);
    }

}