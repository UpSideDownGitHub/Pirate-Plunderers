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
    public GameObject currentButton;

    [Header("Information/Stats Screens")]
    public GameObject[] cannonScreenUI;
    public GameObject[] sailScreenUI;
    public GameObject[] colourScreenUI;
    public GameObject[] sizeScreenUI;

    [Header("Random Items")]
    public int ammount;

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
                currentButton = temp;
                CannonPressed(id);
            });
        }
        else if (typeID == 1)
        {
            temp.GetComponentInChildren<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sails[id].name;
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                currentButton = temp;
                SailPressed(id);
            });
        }
        else if (typeID == 2)
        {
            temp.GetComponentInChildren<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Colours[id].name;
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                currentButton = temp;
                ColourPressed(id);
            });
        }
        else if (typeID == 3)
        {
            temp.GetComponentInChildren<TextMeshProUGUI>().text = SaveData.ShipUpgrade.Sizes[id].name;
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                currentButton = temp;
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

        List<int> itemsToBuy_Type = new List<int>();
        List<int> itemsToBuy_ID = new List<int>();

        foreach (var cannon in SaveData.ShipUpgrade.Cannons)
        {
            if (cannon.buyable && !cannon.unlocked)
            {
                itemsToBuy_Type.Add(0);
                itemsToBuy_ID.Add(cannon.ID);
            }
        }
        foreach (var sail in SaveData.ShipUpgrade.Sails)
        {
            if (sail.buyable && !sail.unlocked)
            {
                itemsToBuy_Type.Add(1);
                itemsToBuy_ID.Add(sail.ID);
            }
        }
        foreach (var colour in SaveData.ShipUpgrade.Colours)
        {
            if (colour.buyable && !colour.unlocked)
            {
                itemsToBuy_Type.Add(2);
                itemsToBuy_ID.Add(colour.ID);
            }
        }
        foreach (var size in SaveData.ShipUpgrade.Sizes)
        {
            if (size.buyable && !size.unlocked)
            {
                itemsToBuy_Type.Add(3);
                itemsToBuy_ID.Add(size.ID);
            }
        }
        try
        {
            
            if (itemsToBuy_Type.Count <= 15)
                ammount = Random.Range(1, itemsToBuy_Type.Count);
            else
                ammount = Random.Range(1, itemsToBuy_Type.Count);
            for (int i = 0; i < ammount; i++)
            {
                int itemToSpawn = Random.Range(0, itemsToBuy_Type.Count);
                PopulateGrid(itemsToBuy_ID[itemToSpawn], itemsToBuy_Type[itemToSpawn]);
                itemsToBuy_Type.RemoveAt(itemToSpawn);
                itemsToBuy_ID.RemoveAt(itemToSpawn);
            }
        }
        catch
        {
            // NO ITEMS LEFT
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
        if (currentItemSelected != 999 & currentUpgradeType != 999)
        {
            var SaveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            if (currentUpgradeType == 0)
            {
                if (SaveData.ShipUpgrade.Cannons[currentItemSelected].buyable && !SaveData.ShipUpgrade.Cannons[currentItemSelected].unlocked)
                {
                    if (SaveData.progression.coins - SaveData.ShipUpgrade.Cannons[currentItemSelected].price >= 0)
                    {
                        SaveData.progression.coins -= (int)SaveData.ShipUpgrade.Cannons[currentItemSelected].price;
                        SaveData.ShipUpgrade.Cannons[currentItemSelected].unlocked = true;
                        SaveData.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
                    }
                }

            }
            else if (currentUpgradeType == 1)
            {
                if (SaveData.ShipUpgrade.Sails[currentItemSelected].buyable && !SaveData.ShipUpgrade.Sails[currentItemSelected].unlocked)
                {
                    if (SaveData.progression.coins - SaveData.ShipUpgrade.Sails[currentItemSelected].price >= 0)
                    {
                        SaveData.progression.coins -= (int)SaveData.ShipUpgrade.Sails[currentItemSelected].price;
                        SaveData.ShipUpgrade.Sails[currentItemSelected].unlocked = true;
                        SaveData.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
                    }
                }
            }
            else if (currentUpgradeType == 2)
            {
                if (SaveData.ShipUpgrade.Colours[currentItemSelected].buyable && !SaveData.ShipUpgrade.Colours[currentItemSelected].unlocked)
                {
                    if (SaveData.progression.coins - SaveData.ShipUpgrade.Colours[currentItemSelected].price >= 0)
                    {
                        SaveData.progression.coins -= (int)SaveData.ShipUpgrade.Colours[currentItemSelected].price;
                        SaveData.ShipUpgrade.Colours[currentItemSelected].unlocked = true;
                        SaveData.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
                    }
                }
            }
            else if (currentUpgradeType == 3)
            {
                if (SaveData.ShipUpgrade.Sizes[currentItemSelected].buyable && !SaveData.ShipUpgrade.Sizes[currentItemSelected].unlocked)
                {
                    if (SaveData.progression.coins - SaveData.ShipUpgrade.Sizes[currentItemSelected].price >= 0)
                    {
                        SaveData.progression.coins -= (int)SaveData.ShipUpgrade.Sizes[currentItemSelected].price;
                        SaveData.ShipUpgrade.Sizes[currentItemSelected].unlocked = true;
                        SaveData.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
                    }
                }
            }
            coins.text = SaveData.progression.coins.ToString();

            Destroy(currentButton);
            currentUpgradeType = 999;
            currentItemSelected = 999;
        }
        
    }

    public void Back()
    {

    }
}
