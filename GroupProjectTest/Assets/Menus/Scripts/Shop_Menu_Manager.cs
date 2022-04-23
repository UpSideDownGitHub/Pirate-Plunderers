using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Menu_Manager : MonoBehaviour
{
    [Header("Button Spawning")]
    public GameObject spawnUnder;
    public GameObject prefabButton;
    int currentUpgradeType = 999;
    int currentItemSelected = 999;

    [Header("Information/Stats Screens")]
    public GameObject[] cannonScreenUI;
    public GameObject[] sailScreenUI;
    public GameObject[] colourScreenUI;
    public GameObject[] sizeScreenUI;

    [Header("Random Items")]
    public int ammount;
    public int minAmmount, maxAmmount;
    public List<int> allreadySpawned = new List<int>();

    [Header("Coins")]
    public TextMeshProUGUI coins;

    public void PopulateGrid(int id, int typeID)
    {
        GameObject temp = Instantiate(prefabButton, spawnUnder.transform);
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        if (typeID == 0)
        {
            temp.GetComponentInChildren<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Cannons[id].name;
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                CannonPressed(id);
            });
        }
        else if (typeID == 1)
        {
            temp.GetComponentInChildren<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sails[id].name;
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                SailPressed(id);
            });
        }
        else if (typeID == 2)
        {
            temp.GetComponentInChildren<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Colours[id].name;
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                ColourPressed(id);
            });
        }
        else if (typeID == 3)
        {
            temp.GetComponentInChildren<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].name;
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
        currentUpgradeType = 0;
        currentItemSelected = id;

        TurnOffAllMenus();
        cannonScreenUI[0].SetActive(true);

        // LOAD SAVE DATA TO GET INFORMATION OUT
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        /*
         *  0 = Main Object
         *  1 = Name
         *  2 = Damage
         *  3 = Range
         *  4 = Description
         *  5 = Price
         */
        // SET THE VALUES IN THE MENU TO THE VALUES THAT ARE HELD IN THE SAVE DATA FILE
        cannonScreenUI[1].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Cannons[id].name;
        cannonScreenUI[2].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Cannons[id].damage.ToString();
        cannonScreenUI[3].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Cannons[id].range.ToString();
        cannonScreenUI[4].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Cannons[id].description;
        cannonScreenUI[5].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Cannons[id].price.ToString();
    }
    public void SailPressed(int id)
    {
        currentUpgradeType = 1;
        currentItemSelected = id;

        TurnOffAllMenus();
        sailScreenUI[0].SetActive(true);

        // LOAD SAVE DATA TO GET INFORMATION OUT
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        /*
         *  0 = Main Object
         *  1 = Name
         *  2 = Speed
         *  3 = Turning
         *  4 = Description
         *  5 = Price
         */
        // SET THE VALUES IN THE MENU TO THE VALUES THAT ARE HELD IN THE SAVE DATA FILE
        sailScreenUI[1].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sails[id].name;
        sailScreenUI[2].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sails[id].speed.ToString();
        sailScreenUI[3].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sails[id].turning.ToString();
        sailScreenUI[4].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sails[id].description;
        sailScreenUI[5].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sails[id].price.ToString();
    }
    public void ColourPressed(int id)
    {
        currentUpgradeType = 2;
        currentItemSelected = id;

        TurnOffAllMenus();
        colourScreenUI[0].SetActive(true);

        // LOAD SAVE DATA TO GET INFORMATION OUT
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        /*
         *  0 = Main Object
         *  1 = Name
         *  2 = Description
         *  3 = Price
         */
        // SET THE VALUES IN THE MENU TO THE VALUES THAT ARE HELD IN THE SAVE DATA FILE
        colourScreenUI[1].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Colours[id].name;
        colourScreenUI[2].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Colours[id].description;
        colourScreenUI[3].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Colours[id].price.ToString();
    }
    public void SizePressed(int id)
    {
        currentUpgradeType = 3;
        currentItemSelected = id;

        TurnOffAllMenus();
        sizeScreenUI[0].SetActive(true);

        // LOAD SAVE DATA TO GET INFORMATION OUT
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        /*
         *  0 = Main Object
         *  1 = Name
         *  2 = Health
         *  3 = Speed
         *  4 = Turning
         *  5 = Description
         *  6 = Price
         */
        // SET THE VALUES IN THE MENU TO THE VALUES THAT ARE HELD IN THE SAVE DATA FILE
        sizeScreenUI[1].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].name;
        sizeScreenUI[2].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].health.ToString();
        sizeScreenUI[3].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].speed.ToString();
        sizeScreenUI[4].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].turning.ToString();
        sizeScreenUI[5].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].description;
        sizeScreenUI[6].GetComponent<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].price.ToString();
    }
    
    public void LoadItems()
    {
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));

        foreach (Transform child in spawnUnder.transform)
        {
            Destroy(child.gameObject);
        }

        ammount = Random.Range(minAmmount, maxAmmount);
        for (int i = 0; i < ammount; i++)
        {
            int type = Random.Range(0, 4);
            int id = 0;
            if (type == 0)
                id = Random.Range(0, SaveData.ShipUpgrade.Cannons.Length);
            else if (type == 1)
                id = Random.Range(0, SaveData.ShipUpgrade.Sails.Length);
            else if (type == 2)
                id = Random.Range(0, SaveData.ShipUpgrade.Colours.Length);
            else if (type == 3)
                id = Random.Range(0, SaveData.ShipUpgrade.Sizes.Length);
            bool allreadyThere = false;
            foreach (int num in allreadySpawned)
            {
                if (num == id)
                {
                    allreadyThere = true;
                    break;
                }
            }
            if (allreadyThere)
                continue;
            allreadySpawned.Add(id);
            PopulateGrid(id, type);
        }
    }

    public void OnEnable()
    {
        var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
        coins.text = SaveData.progression.coins.ToString();
        LoadItems();
    }

    public void Buy()
    {

    }

    public void Back()
    {

    }
}
