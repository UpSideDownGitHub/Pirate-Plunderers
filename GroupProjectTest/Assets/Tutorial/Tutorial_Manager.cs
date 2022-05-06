using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Manager : MonoBehaviour
{
    [Header("Part 1")]
    public bool open1;
    public GameObject door1;
    public GameObject[] dummys;

    [Header("Part 2")]
    public bool open2;
    public GameObject door2;
    public List<GameObject> enemysToSpawn = new List<GameObject>();
    public GameObject curEnemy;
    public GameObject spawnPos;
    int i = 0;
    bool startSpawning;
    public float spawnRadius;

    [Header("Part 3")]
    public bool open3;
    public GameObject door3;
    public EnemyEncounter encounter;

    // Start is called before the first frame update
    void Start()
    {
        open1 = false;
        open2 = false;
        open3 = false;

        startSpawning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            startSpawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!open1)
        {
            foreach (GameObject var in dummys)
            {
                if (var != null)
                {
                    return;
                }
            }
            open1 = true;
            door1.SetActive(false);
        }
        else if (!open2)
        {
            if (startSpawning)
            {
                if (curEnemy == null)
                {
                    if (i > enemysToSpawn.Count-1)
                    {
                        open2 = true;
                        door2.SetActive(false);
                        return;
                    }
                    Vector2 pos = Random.insideUnitCircle * spawnRadius;
                    pos += (Vector2)spawnPos.transform.position;
                    curEnemy = Instantiate(enemysToSpawn[i], pos, Quaternion.identity);
                    i++;
                }
            }
        }
        else if (!open3)
        {
            if (!encounter.once)
            {
                open3 = true;
                door3.SetActive(false);
            }
        }
    }
}
