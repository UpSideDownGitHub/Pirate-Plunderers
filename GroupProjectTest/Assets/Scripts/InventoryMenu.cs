using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryMenu : MonoBehaviour
{


    public static bool GameIsPaused = false;
    public string newGameLevel;
    public GameObject inventoryMenuUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
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
        inventoryMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        inventoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }


   
}




