using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Bullets")]
    public GameObject bullet;
    public GameObject[] firePoints;

    [Header("Shooting")]
    public float bulletForce;

    [Header("Cannon")]
    public GameObject cannonMain;
    public float cannonRotateSpeed;
    public float maxAimDistance = 10;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ROTATE THE CANNON TO FACE THE PLAYER
        Vector3 moveDirection = (this.transform.position - cannonMain.transform.position).normalized;
        Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, moveDirection);
        cannonMain.transform.rotation = Quaternion.RotateTowards(cannonMain.transform.rotation, toRotation2, cannonRotateSpeed * Time.deltaTime);
    }

    public void shoot()
    {
        Transform targetPos = GetClosestEnemy();
        /*
        if (targetPos == null)
        {
            Quaternion direction = new Quaternion(Fire_Points[0].transform.rotation.x, Fire_Points[0].transform.rotation.y, Fire_Points[0].transform.rotation.z, Fire_Points[0].transform.rotation.w);
            GameObject TemporaryBulletHandler = Instantiate(Bullet, Fire_Points[0].transform.position, direction);
            TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.right * Bullet_Force;
        }
        else
        {
            GameObject TemporaryBulletHandler = Instantiate(Bullet, Fire_Points[0].transform.position, Quaternion.identity);
            TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = (targetPos.position - transform.position).normalized * Bullet_Force;
        }
        */
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
