using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public NavMeshAgent NavMesh;
    public float Speed;
    public float Stopping_Distance;
    public float Attack_Distance;

    public GameObject Player;
    public float Damage;
    public bool Attacking;

    public float Attack_Wait_Time;
    public float Attack_Time;

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

        transform.LookAt(Player.transform, Vector3.back);
        transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);

        // in range of the player and can attack them
        if (distance <= Attack_Distance)
        {
            // if not allready attcking the player
            if (!Attacking)
            {
                // attack the player
                InvokeRepeating("Attack", Attack_Wait_Time, Attack_Time);
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
            // set a new destination of the player
            NavMesh.SetDestination(Player.transform.position);
        }
    }

    // the attack function of the ship
    public void Attack()
    {
        Debug.Log("Attack The Player");
    }
}
