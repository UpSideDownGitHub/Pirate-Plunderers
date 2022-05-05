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

    [Header("XP")]
    public XP xp;
    public int xpAmmount;

    // Start is called before the first frame update
    public void Start()
    {
        Current_Health = Max_Health;
        if (partOfEncounter)
            EnemySpawner = GameObject.FindGameObjectWithTag("ENEMYSPAWNER").GetComponent<EnemyEncounter>();
    }

    public void Take_Off_Health(float damage)
    {
        Current_Health -= damage;
        if (Current_Health <= 0)
        {
            Destroy(gameObject);

            GenralSaveContainer saveData = GenralSaveContainer.Load(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            saveData.progression.xp += xpAmmount;
            saveData.Save(Path.Combine(Application.persistentDataPath, "GameSave.xml"));
            xp = GameObject.FindGameObjectWithTag("XP").GetComponent<XP>();
            xp.updateXP();

            if (partOfEncounter && !allreadyDead)
            {
                EnemySpawner.CurrentEnemysDecrease(weightOfEnemy, enemyNumber);
                allreadyDead = true;
            }
        }
    }
}
