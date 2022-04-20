using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_Menu_Manager : MonoBehaviour
{
    public GameObject spawnUnder;
    public GameObject prefabButton;
    int currentUpgradeType = 999;

    public void PopulateViewPort(int id, int typeID)
    {
        GameObject temp = Instantiate(prefabButton, spawnUnder.transform);
        if (typeID == 0)
        {
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                CannonPressed(id);
            });
        }
        else if (typeID == 1)
        {
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                SailPressed(id);
            });
        }
        else if (typeID == 2)
        {
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                ColourPressed(id);
            });
        }
        else if (typeID == 3)
        {
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                SizePressed(id);
            });
        }
    }

    public void CannonPressed(int id)
    {
        // SET THE VARIABLE FOR ALL OF THE INFORMATION IN THE STATS & DESCRIPTION SECTION
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        Debug.Log(id);
        Debug.Log(SaveData.ShipUpgrade.Cannons[id].ID);
    }
    public void SailPressed(int id)
    {
        // SET THE VARIABLE FOR ALL OF THE INFORMATION IN THE STATS & DESCRIPTION SECTION
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        Debug.Log(id);
        Debug.Log(SaveData.ShipUpgrade.Sails[id].ID);
    }
    public void ColourPressed(int id)
    {
        // SET THE VARIABLE FOR ALL OF THE INFORMATION IN THE STATS & DESCRIPTION SECTION
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        Debug.Log(id);
        Debug.Log(SaveData.ShipUpgrade.Colours[id].ID);
    }
    public void SizePressed(int id)
    {
        // SET THE VARIABLE FOR ALL OF THE INFORMATION IN THE STATS & DESCRIPTION SECTION
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        Debug.Log(id);
        Debug.Log(SaveData.ShipUpgrade.Sizes[id].ID);
    }


    public void Equip()
    {

    }

    public void back()
    {

    }

    public void MasterButtonPress(int id)
    {
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        currentUpgradeType = id;
        foreach (Transform child in spawnUnder.transform)
        {
            Destroy(child.gameObject);
        }
        if (id == 0)
        {
            for (int i = 0; i < SaveData.ShipUpgrade.Cannons.Length; i++)
            {
                PopulateViewPort(i, id);
            }
        }
        else if (id == 1)
        {
            for (int i = 0; i < SaveData.ShipUpgrade.Sails.Length; i++)
            {
                Debug.Log(SaveData.ShipUpgrade.Sails[i].ID);
                PopulateViewPort(i, id);
            }
        }
        else if (id == 2)
        {
            for (int i = 0; i < SaveData.ShipUpgrade.Colours.Length; i++)
            {
                Debug.Log(SaveData.ShipUpgrade.Colours[i].ID);
                PopulateViewPort(i, id);
            }
        }
        else if (id == 3)
        {
            for (int i = 0; i < SaveData.ShipUpgrade.Sizes.Length; i++)
            {
                Debug.Log(SaveData.ShipUpgrade.Sizes[i].ID);
                PopulateViewPort(i, id);
            }
        }
    }
}
