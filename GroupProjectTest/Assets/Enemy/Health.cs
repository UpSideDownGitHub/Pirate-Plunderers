using System.Collections;
using System.Collections.Generic;
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
            if (partOfEncounter && !allreadyDead)
            {
                EnemySpawner.CurrentEnemysDecrease(weightOfEnemy, enemyNumber);
                allreadyDead = true;
            }
        }
    }
}
