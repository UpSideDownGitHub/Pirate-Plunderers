using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopIsland : MonoBehaviour
{


    public static bool GameIsPaused = false;
    public string newGameLevel;
    public GameObject shopMenuUI;


    // Update is called once per frame
    void OnTriggerEnter2D (Collider2D collision)
    {
        
            if (Input.GetKeyDown(KeyCode.P))
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
        shopMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        shopMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
}