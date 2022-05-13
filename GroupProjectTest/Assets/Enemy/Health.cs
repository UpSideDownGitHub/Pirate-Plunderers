using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Health : MonoBehaviour
{
    [Header("Health")]
    public int Max_Health;
    public float Current_Health;

    [Header("Spawner Information")]
    public bool partOfEncounter = false;
    public bool allreadyDead = false;
    public EnemyEncounter EnemySpawner;
    public int weightOfEnemy;
    public int enemyNumber;
    public int subNumber;

    [Header("XP")]
    public XP xp;
    public int xpAmmount;

    [Header("Random Enemy Spawner")]
    public Random_Spawns player;
    public bool seenPlayer;
    public int coins;

    [Header("Dummy")]
    public bool dummyEnemy;

    // Start is called before the first frame update
    public void Start()
    {
        Current_Health = Max_Health;
        if (partOfEncounter)
            EnemySpawner = GameObject.FindGameObjectWithTag("ENEMYSPAWNER").GetComponent<EnemyEncounter>();
        else if (!dummyEnemy)
        {
            seenPlayer = false;
            Invoke("noPlayerSoDestroy", 8);
        }

    }

    public void noPlayerSoDestroy()
    {
        seenPlayer |= gameObject.GetComponent<Enemy>().seenPlayer;
        
        if (!seenPlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Random_Spawns>();
            player.decreaseCurrentEnemy();
            allreadyDead = true;
            Destroy(gameObject);
        }
    }

    public void Take_Off_Health(float damage)
    {
        seenPlayer = true;
        Current_Health -= damage;
        if (!dummyEnemy)
            gameObject.GetComponent<Enemy>().shotByPlayer = true;
        if (Current_Health <= 0)
        {
            Destroy(gameObject);

            GenralSaveContainer saveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            saveData.progression.xp += xpAmmount;
            saveData.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            xp = GameObject.FindGameObjectWithTag("XP").GetComponent<XP>();
            xp.updateXP();
            if (dummyEnemy)
            {
                GenralSaveContainer saveData2 = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
                saveData2.progression.coins += coins;
                saveData2.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            }

            if (partOfEncounter && !allreadyDead)
            {
                EnemySpawner.CurrentEnemysDecrease(weightOfEnemy, enemyNumber);
                allreadyDead = true;
            }
            else if (!partOfEncounter && !allreadyDead && !dummyEnemy)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Random_Spawns>();
                player.decreaseCurrentEnemy();
                allreadyDead = true;

                GenralSaveContainer saveData2 = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
                saveData2.progression.coins += coins;
                saveData2.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            }
        }
    }
}
