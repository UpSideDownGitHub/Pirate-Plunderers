using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject[] Fire_Points;
    public float Bullet_Force;

    public float MaxAimDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot()
    {
        Transform targetPos = GetClosestEnemy();

        if (targetPos == null)
        {
            Quaternion direction = new Quaternion(Fire_Points[0].transform.rotation.x, Fire_Points[0].transform.rotation.y, Fire_Points[0].transform.rotation.z, Fire_Points[0].transform.rotation.w);
            GameObject TemporaryBulletHandler = Instantiate(Bullet, Fire_Points[0].transform.position, direction);
            TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.right * Bullet_Force;

            direction = new Quaternion(Fire_Points[1].transform.rotation.x, Fire_Points[1].transform.rotation.y, Fire_Points[1].transform.rotation.z, Fire_Points[1].transform.rotation.w);
            GameObject TemporaryBulletHandler2 = Instantiate(Bullet, Fire_Points[1].transform.position, direction);
            TemporaryBulletHandler2.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.right * Bullet_Force;

            direction = new Quaternion(Fire_Points[2].transform.rotation.x, Fire_Points[2].transform.rotation.y, Fire_Points[2].transform.rotation.z, Fire_Points[2].transform.rotation.w);
            GameObject TemporaryBulletHandler3 = Instantiate(Bullet, Fire_Points[2].transform.position, direction);
            TemporaryBulletHandler3.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.right * Bullet_Force;
        }
        else
        {
            if ((Fire_Points[1].transform.position - targetPos.position).sqrMagnitude < (Fire_Points[3].transform.position - targetPos.position).sqrMagnitude)
            {
                GameObject TemporaryBulletHandler = Instantiate(Bullet, Fire_Points[0].transform.position, Quaternion.identity);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = (targetPos.position - transform.position).normalized * Bullet_Force;

                GameObject TemporaryBulletHandler2 = Instantiate(Bullet, Fire_Points[1].transform.position, Quaternion.identity);
                TemporaryBulletHandler2.GetComponent<Rigidbody2D>().velocity = (targetPos.position - transform.position).normalized * Bullet_Force;

                GameObject TemporaryBulletHandler3 = Instantiate(Bullet, Fire_Points[2].transform.position, Quaternion.identity);
                TemporaryBulletHandler3.GetComponent<Rigidbody2D>().velocity = (targetPos.position - transform.position).normalized * Bullet_Force;
            }
            else
            {
                GameObject TemporaryBulletHandler = Instantiate(Bullet, Fire_Points[3].transform.position, Quaternion.identity);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = (targetPos.position - transform.position).normalized * Bullet_Force;

                GameObject TemporaryBulletHandler2 = Instantiate(Bullet, Fire_Points[4].transform.position, Quaternion.identity);
                TemporaryBulletHandler2.GetComponent<Rigidbody2D>().velocity = (targetPos.position - transform.position).normalized * Bullet_Force;

                GameObject TemporaryBulletHandler3 = Instantiate(Bullet, Fire_Points[5].transform.position, Quaternion.identity);
                TemporaryBulletHandler3.GetComponent<Rigidbody2D>().velocity = (targetPos.position - transform.position).normalized * Bullet_Force;
            }
        }
    }
    Transform GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        return bestTarget;
    }
}
