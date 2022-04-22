using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Customisations : MonoBehaviour
{
    public GameObject[] cannons;
    public GameObject[] sails;

    // Start is called before the first frame update
    void Start()
    {
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));

        for (int i = 0; i < cannons.Length; i++)
        {
            cannons[i].SetActive(false);
        }
        for (int i = 0; i < sails.Length; i++)
        {
            sails[i].SetActive(false);
        }

        cannons[SaveData.loadout.cannon].SetActive(true);
        sails[SaveData.loadout.sail].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
