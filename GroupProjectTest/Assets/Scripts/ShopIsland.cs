using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopIsland : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject shopActiveButton;
    public GameObject shopMenuUI;
    public GameObject homeActiveButton;
    public GameObject homeMenuUI;
    public GameObject joyStick;
    public GameObject shootButton;
    public GameObject miniMap;
    public GameObject xpBar;
    public GameObject healthBar;

    public bool home = false;

    [Header("Customisations")]
    public Customisations custom;


    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (home)
                homeActiveButton.SetActive(true);
            else
                shopActiveButton.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (home)
                homeActiveButton.SetActive(false);
            else
                shopActiveButton.SetActive(false);
        }
    }

    public void ShopExit()
    {
        miniMap.SetActive(true);
        shootButton.SetActive(true);
        joyStick.SetActive(true);
        xpBar.SetActive(true);
        healthBar.SetActive(true);
        //pauseButton.SetActive(true);
        //inventoryButton.SetActive(true);
        shopActiveButton.SetActive(true);
        shopMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ShopEnter()
    {
        miniMap.SetActive(false);
        shootButton.SetActive(false);
        joyStick.SetActive(false);
        xpBar.SetActive(false);
        healthBar.SetActive(false);
        //pauseButton.SetActive(false);
        //inventoryButton.SetActive(false);
        shopActiveButton.SetActive(false);
        shopMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }


    public void homeExit()
    {
        miniMap.SetActive(true);
        shootButton.SetActive(true);
        joyStick.SetActive(true);
        xpBar.SetActive(true);
        healthBar.SetActive(true);
        //pauseButton.SetActive(true);
        //inventoryButton.SetActive(true);
        homeActiveButton.SetActive(true);
        homeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        custom.loadCustomisations();
    }
    public void homeEnter()
    {
        miniMap.SetActive(false);
        shootButton.SetActive(false);
        joyStick.SetActive(false);
        xpBar.SetActive(false);
        healthBar.SetActive(false);
        //pauseButton.SetActive(false);
        //inventoryButton.SetActive(false);
        homeActiveButton.SetActive(false);
        homeMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

}