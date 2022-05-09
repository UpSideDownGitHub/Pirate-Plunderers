using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour
{
     

    public void ChangeScene(int newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

  
}
