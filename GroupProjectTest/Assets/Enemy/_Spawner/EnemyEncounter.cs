using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEncounter : MonoBehaviour
{
    [Header("Score Information")]
    public bool once = true;
    public int[] coins = new int[4]{ 100, 200, 300, 500 };
    public int[] ammountOfEachEnemy = new int[4] { 0, 0, 0, 0 };
    
    public GameObject endScreenUI;
    public int coinsGained;
    public TextMeshProUGUI coinsText;
    public GameObject newUnlock;

    [Header("Encounter Information")]
    public int[] enemyWeights;
    public int[] globalMaxAmountOfEnemy;
    public int[] maxAmmountOfEnemy;
    public int[] ammountOfCurrentEnemy;
    public int[] enemySpawnOrder;

    public int globalMaxEnemys;
    public int maxEnemys;
    public int currentEnemys;

    [Header("Waves")]
    public int currentwave = 1;
    public int maxWaves;
    public bool hasBoss;
    [Range(0,1)]
    public int bossID;
    public int[] ammountOfEnemysToAdd;

    [Header("Spanwing")]
    public Transform[] spawnPositions;
    public float radius;

    [Header("Enimies")]
    /*
     *  0 = small 1
     *  1 = small 2
     *  2 = small 3
     *  3 = medium 1
     *  4 = medium 2
     *  5 = medium 3
     *  6 = large 1
     *  7 = large 2
     *  8 = boss 1
     *  9 = boss 2
     */
    public GameObject[] enimies;

    [Header("UI Elements")]
    public GameObject wavesUI;
    public TextMeshProUGUI waveNumber;
    public Slider waveSlider;
    public float sliderCurrentValue;

    [Header("Tutorial")]
    public bool tutorial;

    EncounterManager encounterManager;

    public void OnEnable()
    {
        encounterManager = GameObject.FindGameObjectWithTag("ENCOUNTERMANAGER").GetComponent<EncounterManager>();
        wavesUI.SetActive(true);
        currentwave = 1;
        maxAmmountOfEnemy = globalMaxAmountOfEnemy;
        maxEnemys = globalMaxEnemys;
        currentEnemys = 0;
        sliderCurrentValue = 0;


        waveNumber.text = "Wave: " + currentwave.ToString();
        sliderCurrentValue = waveSlider.minValue;
        waveSlider.value = 0;
        GenerateEnemys();
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemys == 0 && currentwave <= maxWaves)
        {
            currentwave += 1;
            
            waveNumber.text = "Wave: " + currentwave.ToString();
            sliderCurrentValue = waveSlider.minValue;
            waveSlider.value = 0;

            if (currentwave > maxWaves && hasBoss)
            {
                // SPAWN THE BOSS
                currentEnemys += enemyWeights[3];
                ammountOfCurrentEnemy[3] += 1;
                Spawn(3);
            }
            else
            {
                AddMaxEnemys();
                GenerateEnemys();
            }
        }
        else if (currentEnemys == 0 && currentwave > maxWaves && once)
        {
            // RUN THE END OF THE ROUND CODE AND LET THE PLAYER WIN THE LOOT
            once = false;
            wavesUI.SetActive(false);
            endScreenUI.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                coinsGained += ammountOfEachEnemy[i] * coins[i];
            }
            coinsText.text = "+" + coinsGained.ToString() + " Coins";
            GenralSaveContainer saveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            if (!saveData.progression.firstBossDefeated && hasBoss && bossID == 0)
            {
                newUnlock.SetActive(true);
                saveData.progression.firstBossDefeated = true;
                saveData.ShipUpgrade.Cannons[5].unlocked = true;
            }
            else if (!saveData.progression.finalBossDefeated && hasBoss && bossID == 1)
            {
                newUnlock.SetActive(true);
                saveData.progression.finalBossDefeated = true;
                saveData.ShipUpgrade.Cannons[6].unlocked = true;
            }
            else
                newUnlock.SetActive(false);
            saveData.progression.coins += coinsGained;
            saveData.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            encounterManager.encounterStarted = false;
            gameObject.SetActive(false);
        }
    }

    public void GenerateEnemys()
    {
        foreach (int enemynum in enemySpawnOrder)
        {
            for (int i = 0; i < maxAmmountOfEnemy[enemynum]; i++)
            {
                if (maxAmmountOfEnemy[enemynum] <= ammountOfCurrentEnemy[enemynum] || (currentEnemys + enemyWeights[enemynum]) > maxEnemys)
                {
                    break;
                }
                Spawn(enemynum);
               
                currentEnemys += enemyWeights[enemynum];
                ammountOfCurrentEnemy[enemynum] += 1;
            }
        }
    }

    public void Spawn(int enemyNumber)
    {
        GameObject enemyToSpawn;
        if (enemyNumber == 0)
        {
            ammountOfEachEnemy[0] += 1;
            enemyToSpawn = enimies[Random.Range(0, 3)];
        }
        else if (enemyNumber == 1)
        {
            ammountOfEachEnemy[1] += 1;
            enemyToSpawn = enimies[Random.Range(3, 5)];
        }
        else if (enemyNumber == 2)
        {
            ammountOfEachEnemy[2] += 1;
            enemyToSpawn = enimies[Random.Range(6, 8)]; 
        }
        else if (enemyNumber == 3)
        {
            ammountOfEachEnemy[3] += 1;
            if (bossID == 0)
                enemyToSpawn = enimies[8];
            else
                enemyToSpawn = enimies[9];
        }
        else
            enemyToSpawn = enimies[0];


        int place = Random.Range(0, spawnPositions.Length);
        Vector3 spawnPlace = Random.insideUnitCircle * radius + (Vector2)spawnPositions[place].transform.position;
        spawnPlace.z = -0.3f;
        GameObject temp = Instantiate(enemyToSpawn, spawnPlace, new Quaternion(0, 0, 0, 0));
        temp.GetComponent<Health>().partOfEncounter = true;
        if (tutorial)
        {
            temp.GetComponent<Health>().dummyEnemy = true;
            temp.GetComponent<Health>().Max_Health = 10;
            temp.GetComponent<Health>().Current_Health = 10;
        }
    }
    public void AddMaxEnemys()
    {
        maxEnemys += 3;
        for (int i = 0; i < maxAmmountOfEnemy.Length; i++)
        {
            maxAmmountOfEnemy[i] += ammountOfEnemysToAdd[i];
        }
    }
    public void CurrentEnemysDecrease(int weightOfEnemy, int enemyNumber)
    {
        currentEnemys -= weightOfEnemy;
        ammountOfCurrentEnemy[enemyNumber] -= 1;

        sliderCurrentValue = ((float)(maxEnemys-currentEnemys) / (float)maxEnemys) * 100.00f;
        waveSlider.value = sliderCurrentValue;
    }
}
