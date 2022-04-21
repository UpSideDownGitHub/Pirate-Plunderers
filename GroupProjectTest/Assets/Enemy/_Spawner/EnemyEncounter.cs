using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEncounter : MonoBehaviour
{
    public bool startEncounter = false;

    [Header("Encounter Information")]
    public int[] enemyWeights;
    public int[] maxAmmountOfEnemy;
    public int[] ammountOfCurrentEnemy;
    public int[] enemySpawnOrder;

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

    void Start()
    {
        wavesUI.SetActive(true);
        waveNumber.text = "Wave: " + currentwave.ToString();
        sliderCurrentValue = waveSlider.minValue;
        waveSlider.value = 0;
        GenerateEnemys();
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
        else if (currentEnemys == 0 && currentwave > maxWaves)
        {
            // RUN THE END OF THE ROUND CODE AND LET THE PLAYER WIN THE LOOT
            wavesUI.SetActive(false);
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
                Debug.Log("Weight: " + enemyWeights[enemynum].ToString());
               
                currentEnemys += enemyWeights[enemynum];
                ammountOfCurrentEnemy[enemynum] += 1;

                Debug.Log("Current Ammount: " + ammountOfCurrentEnemy[enemynum].ToString());
                Debug.Log("Current Enemies: " + currentEnemys.ToString());
                Debug.Log("\n");
            }
        }
    }

    public void Spawn(int enemyNumber)
    {
        GameObject enemyToSpawn;
        if (enemyNumber == 0)
            enemyToSpawn = enimies[Random.Range(0, 2)];
        else if (enemyNumber == 1)
            enemyToSpawn = enimies[Random.Range(3, 5)];
        else if (enemyNumber == 2)
            enemyToSpawn = enimies[Random.Range(6, 7)];
        else if (enemyNumber == 3)
        {
            if (bossID == 0)
                enemyToSpawn = enimies[8];
            else
                enemyToSpawn = enimies[9];
        }
        else
            enemyToSpawn = enimies[0];

        Debug.Log("Enemy Name: " + enemyToSpawn.name);

        int place = Random.Range(0, spawnPositions.Length);
        Vector3 spawnPlace = Random.insideUnitCircle * radius + (Vector2)spawnPositions[place].transform.position;
        spawnPlace.z = -0.3f;
        GameObject temp = Instantiate(enemyToSpawn, spawnPlace, new Quaternion(0, 0, 0, 0));
        temp.GetComponent<Health>().partOfEncounter = true;
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
