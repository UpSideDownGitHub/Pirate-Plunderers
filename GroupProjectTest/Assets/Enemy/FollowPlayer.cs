using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    [Header("Nav Mesh")]
    public NavMeshAgent NavMesh;
    public float Speed;
    public float Stopping_Distance;
    public float Attack_Distance;

    [Header("Attacking")]
    public GameObject Player;
    public float Damage;
    public bool Attacking;
    public float Attack_Time;

    [Header("Shooting")]
    public GameObject[] Fire_Points;
    public GameObject bullet;
    public float bulletForce;

    [Header("Rotation")]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Player = GameObject.FindGameObjectWithTag("Player");
        NavMesh.speed = Speed;
        NavMesh.stoppingDistance = Stopping_Distance;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (transform.rotation.x != 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        */
        // if there is not player to attack/follow
        if (!Player)
        {
            // return to avoid errors
            return;
        }

        // get the distance to the player
        var distance = Vector2.Distance(transform.position, Player.transform.position);

        // look at player
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
        /*
        Vector2 moveDirection;
        try
        {
            moveDirection = (transform.position - path.corners[1]).normalized;
        }
        catch
        {
            moveDirection = (transform.position - path.corners[0]).normalized;
        }
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        */


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

    // the attack function of the ship
    public void Attack()
    {
        if ((Fire_Points[0].transform.position - Player.transform.position).sqrMagnitude < (Fire_Points[1].transform.position - Player.transform.position).sqrMagnitude)
        {
            Quaternion direction = new Quaternion(Fire_Points[0].transform.rotation.x, Fire_Points[0].transform.rotation.y, Fire_Points[0].transform.rotation.z, Fire_Points[0].transform.rotation.w);
            GameObject TemporaryBulletHandler = Instantiate(bullet, Fire_Points[0].transform.position, direction);
            TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.right * bulletForce;

        }
        else
        {
            Quaternion direction = new Quaternion(Fire_Points[1].transform.rotation.x, Fire_Points[1].transform.rotation.y, Fire_Points[1].transform.rotation.z, Fire_Points[1].transform.rotation.w);
            GameObject TemporaryBulletHandler = Instantiate(bullet, Fire_Points[1].transform.position, direction);
            TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = -TemporaryBulletHandler.transform.right * bulletForce;

        }
    }
}
