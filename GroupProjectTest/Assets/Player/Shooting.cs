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
    public float shootTime;
    public bool reload;
    bool reloading = true;
    public float shootingTime;
    public float reloadTime;
    bool canShoot = true;

    [Header("Cannon")]
    public Joystick aimingJoystick;
    public Joystick movementJoystick;
    public GameObject cannonMain;
    public float cannonRotateSpeed;
    public bool doOnce = true;
    Vector2 previous;

    [Header("Effects")]
    public GameObject shootEffect;

    [Header("Sounds")]
    public AudioClip shootingSoundEffect;
    [Range(0, 1)]
    public float volume;
    public Sounds soundManager;



    // Start is called before the first frame update
    public void OnEnable()
    {
        canShoot = true;
        reloading = true;
    }

    // Update is called once per frame
    void Update()
    {
        // ROTATE THE CANNON TO FACE CORRECT DIRECTION
        Vector2 moveDirection = aimingJoystick.joystickVec;
        if (moveDirection.Equals(new Vector2(0, 0)))
        {
            moveDirection = movementJoystick.joystickVec;
            if (!moveDirection.Equals(new Vector2(0, 0)))
            {
                previous = moveDirection;
                Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, moveDirection);
                cannonMain.transform.rotation = Quaternion.RotateTowards(cannonMain.transform.rotation, toRotation2, cannonRotateSpeed * Time.deltaTime);
            }
            else
            {
                Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, previous);
                cannonMain.transform.rotation = Quaternion.RotateTowards(cannonMain.transform.rotation, toRotation2, cannonRotateSpeed * Time.deltaTime);
            }
        }
        else
        {
            previous = moveDirection;
            Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, moveDirection);
            cannonMain.transform.rotation = Quaternion.RotateTowards(cannonMain.transform.rotation, toRotation2, cannonRotateSpeed * Time.deltaTime);
        }


        if (aimingJoystick.currentlyShooting && doOnce)
        {
            doOnce = false;
            InvokeRepeating("shoot", 0, shootTime);
        }
        else if (!aimingJoystick.currentlyShooting)
        {
            doOnce = true;
            CancelInvoke("shoot");
        }
    }
    public IEnumerator reloadCycle()
    {
        reloading = false;
        yield return new WaitForSeconds(shootingTime);
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        reloading = true;
    }

    public void shoot()
    {
        //Transform targetPos = GetClosestEnemy();
        if (canShoot && reload && reloading)
        {
            StartCoroutine(reloadCycle());
        }

        if (canShoot)
        {
            for (int i = 0; i < firePoints.Length; i++)
            {
                Quaternion direction = new Quaternion(firePoints[i].transform.rotation.x, firePoints[i].transform.rotation.y, firePoints[i].transform.rotation.z, firePoints[i].transform.rotation.w);
                GameObject TemporaryBulletHandler = Instantiate(bullet, firePoints[i].transform.position, direction);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.up * bulletForce;
                Instantiate(shootEffect, firePoints[i].transform);
            }
            soundManager.playSoundEffect(shootingSoundEffect, volume);
        }



        /*
        else
        {
            GameObject TemporaryBulletHandler = Instantiate(Bullet, Fire_Points[0].transform.position, Quaternion.identity);
            TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = (targetPos.position - transform.position).normalized * Bullet_Force;
        }
        */
    }
    /*
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
    */
}
