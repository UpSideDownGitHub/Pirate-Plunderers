using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LargeEnemy1 : MonoBehaviour
{
    [Header("Nav Mesh")]
    public NavMeshAgent NavMesh;
    public float Speed;

    public float maxdistacne;
    public float Stopping_Distance;

    public float Max_Cannon_Attack_Distance;

    [Header("Attacking")]
    public GameObject Player;
    public bool Attacking;
    public float Attack_Time;
    public float Attack_Time2;

    [Header("Shooting")]
    [Header("Cannon")]
    public GameObject FirePointMaster;
    public GameObject[] firePoints;
    public GameObject bullet;
    public float bulletForce;
    public float cannonRotateSpeed;
    public float cannon_RandomDeviation;

    [Header("Side Cannons")]
    public GameObject[] sideCannonFirePoints;
    public float sideCannonForce;
    public float sideCannon_RandomDeviation;

    [Header("Rotation")]
    public float rotationSpeed;

    [Header("Seen Player")]
    public bool seenPlayer;

    // Start is called before the first frame update
    void Start()
    {
        var agent = GetComponentInChildren<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        seenPlayer = false;

        Player = GameObject.FindGameObjectWithTag("Player");
        NavMesh.speed = Speed;
        NavMesh.stoppingDistance = Stopping_Distance;
    }

    // Update is called once per frame
    void Update()
    {
        // if there is not player to attack/follow
        if (!Player)
        {
            // return to avoid errors
            return;
        }

        // get the distance to the player
        var distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance < maxdistacne)
        {
            seenPlayer = true;
            // ROTATION
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, NavMesh.velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // ROTATE THE CANNON TO FACE THE PLAYER
            Vector3 moveDirection = (Player.transform.position - FirePointMaster.transform.position).normalized;
            Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, moveDirection);
            FirePointMaster.transform.rotation = Quaternion.RotateTowards(FirePointMaster.transform.rotation, toRotation2, cannonRotateSpeed * Time.deltaTime);

            // WHEN THE VELOCITY IS ZERO (AT END OF THE PATH NEED TO ROTATE SO THE SIDE OF THE SHIP IS FACING THE PLAYER SO I CAN
            // SHOOT AT THE PLAYER)


            // in range of the player and can attack them
            if (distance <= Max_Cannon_Attack_Distance)
            {
                // if not allready attcking the player
                if (!Attacking)
                {
                    // attack the player
                    InvokeRepeating("Attack", 0, Attack_Time);
                    InvokeRepeating("SideAttack", 0, Attack_Time2);
                }
                Attacking = true;
            }
            else
            {
                // if attacking the player and the ship is now out of range of the player
                if (Attacking)
                {
                    // stop attacking the player
                    CancelInvoke("Attack");
                    CancelInvoke("SideAttack");
                }
                Attacking = false;
            }

            // set a new destination of the player
            NavMesh.SetDestination(Player.transform.position);
        }
        else
        {
            NavMesh.ResetPath();
            CancelInvoke("Attack");
            CancelInvoke("SideAttack");
            Attacking = false;
        }
    }

    public void SideAttack()
    {
        if ((sideCannonFirePoints[1].transform.position - Player.transform.position).sqrMagnitude < (sideCannonFirePoints[5].transform.position - Player.transform.position).sqrMagnitude)
        {
            for (int i = 0; i < 4; i++)
            {
                Quaternion direction = new Quaternion(sideCannonFirePoints[i].transform.rotation.x, sideCannonFirePoints[i].transform.rotation.y, sideCannonFirePoints[1].transform.rotation.z + Random.Range(-sideCannon_RandomDeviation, sideCannon_RandomDeviation), sideCannonFirePoints[1].transform.rotation.w);
                GameObject TemporaryBulletHandler = Instantiate(bullet, sideCannonFirePoints[i].transform.position, direction);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = -TemporaryBulletHandler.transform.right * sideCannonForce;
            }
        }
        else
        {
            for (int i = 4; i < 8; i++)
            {
                Quaternion direction = new Quaternion(sideCannonFirePoints[i].transform.rotation.x, sideCannonFirePoints[i].transform.rotation.y, sideCannonFirePoints[1].transform.rotation.z + Random.Range(-sideCannon_RandomDeviation, sideCannon_RandomDeviation), sideCannonFirePoints[1].transform.rotation.w);
                GameObject TemporaryBulletHandler = Instantiate(bullet, sideCannonFirePoints[i].transform.position, direction);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.right * sideCannonForce;
            }
        }
    }
    // the attack function of the ship
    public void Attack()
    {
        // FRONT FACING CANNONS
        for (int i = 0; i < 4; i++)
        {
            Quaternion direction3 = new Quaternion(firePoints[i].transform.rotation.x, firePoints[i].transform.rotation.y, firePoints[i].transform.rotation.z + Random.Range(-cannon_RandomDeviation, cannon_RandomDeviation), firePoints[i].transform.rotation.w);
            GameObject TemporaryBulletHandler3 = Instantiate(bullet, firePoints[i].transform.position, direction3);
            TemporaryBulletHandler3.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler3.transform.up * bulletForce;
        }
    }
}
