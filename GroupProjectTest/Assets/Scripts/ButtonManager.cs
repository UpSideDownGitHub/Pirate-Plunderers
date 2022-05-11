using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class ButtonManager : MonoBehaviour
{
    public void ChangeScene(int newGameLevel)
    {
        GenralSaveContainer saveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        if (saveData.progression.firstTime)
            newGameLevel = 2;
        SceneManager.LoadScene(newGameLevel);
    }
}
