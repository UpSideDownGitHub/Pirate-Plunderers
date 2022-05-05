using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemy : MonoBehaviour
{
    [Header("Nav Mesh")]
    public NavMeshAgent NavMesh;
    public float Speed;

    public float maxdistacne;
    public float Stopping_Distance;


    public float Attack_Distance;

    [Header("Attacking")]
    public GameObject Player;
    public float Damage;
    public bool Attacking;
    public float Attack_Time;

    [Header("Shooting")]
    public GameObject firePoint;
    public GameObject bullet;
    public float bulletForce;
    public float cannonRotateSpeed;
    public float randomDeviation;

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
            // TO DRAW THE PATH THAT IS CURRENTLY BEING FOLLOWED
            /*
            var path = NavMesh.path;
            for (int i = 0; i < path.corners.Length - 1; i++)
            { 
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
            */

            // ROTATION
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, NavMesh.velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // ROTATE THE CANNON TO FACE THE PLAYER
            Vector3 moveDirection = (Player.transform.position - firePoint.transform.position).normalized;

            Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, moveDirection);
            firePoint.transform.rotation = Quaternion.RotateTowards(firePoint.transform.rotation, toRotation2, cannonRotateSpeed * Time.deltaTime);

            // WHEN THE VELOCITY IS ZERO (AT END OF THE PATH NEED TO ROTATE SO THE SIDE OF THE SHIP IS FACING THE PLAYER SO I CAN
            // SHOOT AT THE PLAYER)


            // in range of the player and can attack them
            if (distance <= Attack_Distance)
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
        Quaternion direction = new Quaternion(firePoint.transform.rotation.x , firePoint.transform.rotation.y, firePoint.transform.rotation.z + Random.Range(-randomDeviation, randomDeviation), firePoint.transform.rotation.w);
        GameObject TemporaryBulletHandler = Instantiate(bullet, firePoint.transform.position, direction);
        TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.up * bulletForce;

    }
}
