using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class ButtonManager : MonoBehaviour
{
    public GameObject Options;
    public GameObject[] otherButtons;

    public void closeOptions()
    {
        foreach (var item in otherButtons)
        {
            item.SetActive(true);
        }
        Options.SetActive(false);
    }
    public void openOptions()
    {
        foreach (var item in otherButtons)
        {
            item.SetActive(false);
        }
        Options.SetActive(true);
    }



    public void ChangeScene(int newGameLevel)
    {
        GenralSaveContainer saveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        if (saveData.progression.firstTime)
            newGameLevel = 3;
        SceneManager.LoadScene(newGameLevel);
    }
}
