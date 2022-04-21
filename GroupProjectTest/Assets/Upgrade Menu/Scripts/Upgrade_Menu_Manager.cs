using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_Menu_Manager : MonoBehaviour
{
    [Header("Button Spawning")]
    public GameObject spawnUnder;
    public GameObject prefabButton;
    int currentUpgradeType = 999;
    int[] currentItemSelected = { 999,999,999,999};

    [Header("Information/Stats Screens")]
    public GameObject[] cannonScreenUI;
    public GameObject[] sailScreenUI;
    public GameObject[] colourScreenUI;
    public GameObject[] sizeScreenUI;

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

    public void TurnOffAllMenus()
    {
        cannonScreenUI[0].SetActive(false);
        sailScreenUI[0].SetActive(false);
        colourScreenUI[0].SetActive(false);
        sizeScreenUI[0].SetActive(false);
    }

    public void CannonPressed(int id)
    {
        // TURN OFF MENUS
        TurnOffAllMenus();
        cannonScreenUI[0].SetActive(true);

        // LOAD SAVE DATA TO GET INFORMATION OUT
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));

        // SET CURRENT ITEM TO BE SELECTED
        currentItemSelected[0] = id;
        /*
         *  0 = Main Object
         *  1 = Damage
         *  2 = Range
         *  3 = Description
         */
        // SET THE VALUES IN THE MENU TO THE VALUES THAT ARE HELD IN THE SAVE DATA FILE
        cannonScreenUI[1].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Cannons[id].damage.ToString();
        cannonScreenUI[2].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Cannons[id].range.ToString();
        cannonScreenUI[3].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Cannons[id].description.ToString();
    }
    public void SailPressed(int id)
    {
        // TURN OFF MENUS
        TurnOffAllMenus();
        sailScreenUI[0].SetActive(true);

        // LOAD SAVE DATA TO GET INFORMATION OUT
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));

        // SET CURRENT ITEM TO BE SELECTED
        currentItemSelected[1] = id;
        /*
         *  0 = Main Object
         *  1 = Speed
         *  2 = Turning
         *  3 = Description
         */
        // SET THE VALUES IN THE MENU TO THE VALUES THAT ARE HELD IN THE SAVE DATA FILE
        sailScreenUI[1].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sails[id].speed.ToString();
        sailScreenUI[2].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sails[id].turning.ToString();
        sailScreenUI[3].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sails[id].description.ToString();
    }
    public void ColourPressed(int id)
    {
        // TURN OFF MENUS
        TurnOffAllMenus();
        colourScreenUI[0].SetActive(true);

        // LOAD SAVE DATA TO GET INFORMATION OUT
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));

        // SET CURRENT ITEM TO BE SELECTED
        currentItemSelected[2] = id;
        /*
         *  0 = Main Object
         *  1 = Description
         */
        // SET THE VALUES IN THE MENU TO THE VALUES THAT ARE HELD IN THE SAVE DATA FILE
        colourScreenUI[1].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Colours[id].description.ToString();
    }
    public void SizePressed(int id)
    {
        // TURN OFF MENUS
        TurnOffAllMenus();
        sizeScreenUI[0].SetActive(true);

        // LOAD SAVE DATA TO GET INFORMATION OUT
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));

        // SET CURRENT ITEM TO BE SELECTED
        currentItemSelected[3] = id;
        /*
         *  0 = Main Object
         *  1 = Health
         *  2 = Speed
         *  3 = Turning
         *  4 = Description
         */
        // SET THE VALUES IN THE MENU TO THE VALUES THAT ARE HELD IN THE SAVE DATA FILE
        sizeScreenUI[1].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].health.ToString();
        sizeScreenUI[2].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].speed.ToString();
        sizeScreenUI[3].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].turning.ToString();
        sizeScreenUI[4].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].description.ToString();
    }


    public void Equip()
    {
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        if (currentUpgradeType == 0 && currentItemSelected[0] != 999)
        {
            SaveData.loadout.cannon = currentItemSelected[0];
        }
        else if (currentUpgradeType == 1 && currentItemSelected[1] != 999)
        {
            SaveData.loadout.sail = currentItemSelected[1];
        }
        else if (currentUpgradeType == 2 && currentItemSelected[2] != 999)
        {
            SaveData.loadout.colour = currentItemSelected[2];
        }
        else if (currentUpgradeType == 3 && currentItemSelected[3] != 999)
        {
            SaveData.loadout.size = currentItemSelected[3];
        }
        SaveData.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
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
                if (SaveData.ShipUpgrade.Cannons[i].unlocked)
                    PopulateViewPort(i, id);
            }
        }
        else if (id == 1)
        {
            for (int i = 0; i < SaveData.ShipUpgrade.Sails.Length; i++)
            {
                if (SaveData.ShipUpgrade.Sails[i].unlocked)
                    PopulateViewPort(i, id);
            }
        }
        else if (id == 2)
        {
            for (int i = 0; i < SaveData.ShipUpgrade.Colours.Length; i++)
            {
                if (SaveData.ShipUpgrade.Colours[i].unlocked)
                    PopulateViewPort(i, id);
            }
        }
        else if (id == 3)
        {
            for (int i = 0; i < SaveData.ShipUpgrade.Sizes.Length; i++)
            {
                if (SaveData.ShipUpgrade.Sizes[i].unlocked)
                    PopulateViewPort(i, id);
            }
        }
    }
}
