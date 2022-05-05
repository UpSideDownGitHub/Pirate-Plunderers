using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Random_Spawns : MonoBehaviour
{
    [Header("Enemys")]
    public GameObject[] Enemeies;
    /*  0 = small 1
     *  1 = small 2
     *  2 = small 3
     *  3 = medium 1
     *  4 = medium 2
     *  5 = medium 3
     *  6 = large 1
     *  7 = large 2 */

    [Header("Spawner Information")]
    public int currentEnemies = 0;
    public bool canSpawn = true;
    public float waitTimeMin, waitTimeMax;

    [Header("Spawn Chances")]
    public int minSmall;
    public int maxSmall;
    public int minMedium, maxMedium;
    public int minLarge, maxLarge;

    [Header("Spawner Radius")]
    public float minRadius;
    public float maxRadius;
    public float maxRandomPlace;
    public Vector2 randomRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void spawn()
    {
        Vector2 point;
        do
        {
            point = Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
            point += (Vector2)transform.position;
        } while (!NavMesh.SamplePosition(new Vector3(point.x, point.y, 0), out _, 0, NavMesh.AllAreas));

        List<int> toSpawn = new List<int>();
        int ran = Random.Range(0, 8);
        if (ran >= 0 && ran <= 2)
        {
            // SMALL ENEMIES
            toSpawn.Add(ran);
            for (int i = 0; i < Random.Range(minSmall, maxSmall); i++)
            {
                toSpawn.Add(Random.Range(0,3));
            }
        }
        else if (ran >= 3 && ran <= 5)
        {
            // MEDIUM ENEMIES
            toSpawn.Add(ran);
            for (int i = 0; i < Random.Range(minMedium, maxMedium); i++)
            {
                toSpawn.Add(Random.Range(3, 6));
            }
        }
        else if (ran >= 6 && ran <= 7)
        {
            // LARGE ENEMIES
            toSpawn.Add(ran);
            for (int i = 0; i < Random.Range(minLarge, maxLarge); i++)
            {
                toSpawn.Add(Random.Range(6, 8));
            }
        }

        foreach (int enemy in toSpawn)
        {
            currentEnemies++;
            do
            {
                randomRange.x += Random.Range(-maxRandomPlace, maxRandomPlace);
                randomRange.y += Random.Range(-maxRandomPlace, maxRandomPlace);
                point += randomRange;
            } while (!NavMesh.SamplePosition(new Vector3(point.x, point.y, 0), out _, 0, NavMesh.AllAreas));
            Instantiate(Enemeies[enemy], point, Quaternion.identity);
        }
    }
    
    public IEnumerator noSpawns()
    {
        canSpawn = false;
        yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemies == 0 && canSpawn)
        {
            spawn();
            StartCoroutine(noSpawns());
        }
    }

    public void decreaseCurrentEnemy()
    {
        currentEnemies--;
    }
}
