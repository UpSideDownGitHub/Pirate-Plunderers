using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MediumEnemy3 : MonoBehaviour
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

    [Header("Shooting")]
    [Header("Cannon")]
    public GameObject cannonFirePoint;
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

    // Start is called before the first frame update
    void Start()
    {
        var agent = GetComponentInChildren<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

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
            // ROTATION
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, NavMesh.velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // ROTATE THE CANNON TO FACE THE PLAYER
            Vector3 moveDirection = (Player.transform.position - cannonFirePoint.transform.position).normalized;
            Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, moveDirection);
            cannonFirePoint.transform.rotation = Quaternion.RotateTowards(cannonFirePoint.transform.rotation, toRotation2, cannonRotateSpeed * Time.deltaTime);

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
            Attacking = false;
        }
    }

    // the attack function of the ship
    public void Attack()
    {
        if ((sideCannonFirePoints[0].transform.position - Player.transform.position).sqrMagnitude < (sideCannonFirePoints[2].transform.position - Player.transform.position).sqrMagnitude)
        {
            Quaternion direction = new Quaternion(sideCannonFirePoints[0].transform.rotation.x, sideCannonFirePoints[0].transform.rotation.y, sideCannonFirePoints[0].transform.rotation.z + Random.Range(-sideCannon_RandomDeviation, sideCannon_RandomDeviation), sideCannonFirePoints[0].transform.rotation.w);
            GameObject TemporaryBulletHandler = Instantiate(bullet, sideCannonFirePoints[0].transform.position, direction);
            TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = -TemporaryBulletHandler.transform.right * bulletForce;

            Quaternion direction2 = new Quaternion(sideCannonFirePoints[1].transform.rotation.x, sideCannonFirePoints[1].transform.rotation.y, sideCannonFirePoints[1].transform.rotation.z + Random.Range(-sideCannon_RandomDeviation, sideCannon_RandomDeviation), sideCannonFirePoints[1].transform.rotation.w);
            GameObject TemporaryBulletHandler2 = Instantiate(bullet, sideCannonFirePoints[1].transform.position, direction2);
            TemporaryBulletHandler2.GetComponent<Rigidbody2D>().velocity = -TemporaryBulletHandler2.transform.right * bulletForce;
        }
        else
        {
            Quaternion direction = new Quaternion(sideCannonFirePoints[2].transform.rotation.x, sideCannonFirePoints[2].transform.rotation.y, sideCannonFirePoints[2].transform.rotation.z + Random.Range(-sideCannon_RandomDeviation, sideCannon_RandomDeviation), sideCannonFirePoints[2].transform.rotation.w);
            GameObject TemporaryBulletHandler = Instantiate(bullet, sideCannonFirePoints[2].transform.position, direction);
            TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.right * bulletForce;

            Quaternion direction2 = new Quaternion(sideCannonFirePoints[3].transform.rotation.x, sideCannonFirePoints[3].transform.rotation.y, sideCannonFirePoints[3].transform.rotation.z + Random.Range(-sideCannon_RandomDeviation, sideCannon_RandomDeviation), sideCannonFirePoints[3].transform.rotation.w);
            GameObject TemporaryBulletHandler2 = Instantiate(bullet, sideCannonFirePoints[3].transform.position, direction2);
            TemporaryBulletHandler2.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler2.transform.right * bulletForce;
        }
        Quaternion direction3 = new Quaternion(cannonFirePoint.transform.rotation.x, cannonFirePoint.transform.rotation.y, cannonFirePoint.transform.rotation.z + Random.Range(-cannon_RandomDeviation, cannon_RandomDeviation), cannonFirePoint.transform.rotation.w);
        GameObject TemporaryBulletHandler3 = Instantiate(bullet, cannonFirePoint.transform.position, direction3);
        TemporaryBulletHandler3.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler3.transform.up * bulletForce;
    }
}
