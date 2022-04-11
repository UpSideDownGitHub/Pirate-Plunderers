//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RandomEnimies : MonoBehaviour
{
    public GameObject Enemy;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject()
    {
        Vector3 pos = transform.position;
        pos.x += Random.Range(-200, 200);
        pos.y += Random.Range(-200, 200);
        Instantiate(Enemy, pos, transform.rotation);
        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
// Update is called once per frame

