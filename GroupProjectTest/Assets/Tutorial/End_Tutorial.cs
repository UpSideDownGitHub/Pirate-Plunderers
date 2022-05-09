using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Tutorial : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GenralSaveContainer saveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            if (saveData.progression.firstTime)
                saveData.progression.firstTime = false;
            saveData.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            SceneManager.LoadSceneAsync(0);
        }
    }
}
