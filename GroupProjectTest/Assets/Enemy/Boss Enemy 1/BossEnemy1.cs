using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy1 : MonoBehaviour
{
    [Header("Nav Mesh")]
    public NavMeshAgent NavMesh;
    public float Speed;
    public float acceleration;

    public float maxdistacne;
    public float Stopping_Distance;

    public float Max_Cannon_Attack_Distance;
    public float Min_Cannon_Attack_Distance;

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

    public bool arrows;

    [Header("Arrow")]
    public GameObject[] arrowFirePoints;
    public GameObject arrow;
    public float arrowForce;
    public float arrow_RandomDeviation;

    [Header("Ram")]
    public float Max_Ram_Distance;
    public float Min_Ram_Distance;
    public float ramSpeed;
    public float ramAcceleration;
    public float ramCoolDown;
    public float ramTime;
    public bool ram = false;
    public bool canRam = true;

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
        NavMesh.acceleration = acceleration;
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
                if (distance <= Min_Cannon_Attack_Distance)
                    arrows = true;
                else
                    arrows = false;

                if (distance <= Max_Ram_Distance && distance >= Min_Ram_Distance && !ram && canRam)
                    ram = true;
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

    public IEnumerator ramAttack()
    {
        ram = true;
        canRam = false;
        Debug.Log("Ramming");
        NavMesh.speed = ramSpeed;
        NavMesh.acceleration = ramAcceleration;
        yield return new WaitForSeconds(ramTime);
        NavMesh.speed = Speed;
        NavMesh.acceleration = acceleration;
        ram = false;
        yield return new WaitForSeconds(ramCoolDown);
        canRam = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ram)
        {
            if (collision.collider.tag == "Player")
            {
                Debug.Log("Damage Player");
            }
        }
    }

    // the attack function of the ship
    public void Attack()
    {
        if (ram)
        {
            StartCoroutine(ramAttack());
        }
        else if (arrows)
        {
            if ((arrowFirePoints[1].transform.position - Player.transform.position).sqrMagnitude < (arrowFirePoints[4].transform.position - Player.transform.position).sqrMagnitude)
            {
                float ran = Random.Range(-arrow_RandomDeviation, arrow_RandomDeviation);
                Vector3 position = Player.transform.position;
                position.x += ran;
                position.y += ran;

                Vector3 vector = (position - arrowFirePoints[0].transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, vector);
                GameObject TemporaryBulletHandler = Instantiate(arrow, arrowFirePoints[0].transform.position, lookRotation);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = (position - arrowFirePoints[0].transform.position).normalized * arrowForce;


                ran = Random.Range(-arrow_RandomDeviation, arrow_RandomDeviation);
                position = Player.transform.position;
                position.x += ran;
                position.y += ran;
                vector = (position - arrowFirePoints[1].transform.position).normalized;
                lookRotation = Quaternion.LookRotation(Vector3.forward, vector);
                GameObject TemporaryBulletHandler2 = Instantiate(arrow, arrowFirePoints[1].transform.position, lookRotation);
                TemporaryBulletHandler2.GetComponent<Rigidbody2D>().velocity = (position - arrowFirePoints[1].transform.position).normalized * arrowForce;


                ran = Random.Range(-arrow_RandomDeviation, arrow_RandomDeviation);
                position = Player.transform.position;
                position.x += ran;
                position.y += ran;
                vector = (position - arrowFirePoints[2].transform.position).normalized;
                lookRotation = Quaternion.LookRotation(Vector3.forward, vector);
                GameObject TemporaryBulletHandler3 = Instantiate(arrow, arrowFirePoints[2].transform.position, lookRotation);
                TemporaryBulletHandler3.GetComponent<Rigidbody2D>().velocity = (position - arrowFirePoints[2].transform.position).normalized * arrowForce;
            }
            else
            {
                float ran = Random.Range(-arrow_RandomDeviation, arrow_RandomDeviation);
                Vector3 position = Player.transform.position;
                position.x += ran;
                position.y += ran;
                Vector3 vector = (position - arrowFirePoints[3].transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, vector);
                GameObject TemporaryBulletHandler = Instantiate(arrow, arrowFirePoints[3].transform.position, lookRotation);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = (position - arrowFirePoints[3].transform.position).normalized * arrowForce;

                ran = Random.Range(-arrow_RandomDeviation, arrow_RandomDeviation);
                position = Player.transform.position;
                position.x += ran;
                position.y += ran;
                vector = (position - arrowFirePoints[4].transform.position).normalized;
                lookRotation = Quaternion.LookRotation(Vector3.forward, vector);
                GameObject TemporaryBulletHandler2 = Instantiate(arrow, arrowFirePoints[4].transform.position, lookRotation);
                TemporaryBulletHandler2.GetComponent<Rigidbody2D>().velocity = (position - arrowFirePoints[4].transform.position).normalized * arrowForce;

                ran = Random.Range(-arrow_RandomDeviation, arrow_RandomDeviation);
                position = Player.transform.position;
                position.x += ran;
                position.y += ran;
                vector = (position - arrowFirePoints[5].transform.position).normalized;
                lookRotation = Quaternion.LookRotation(Vector3.forward, vector);
                GameObject TemporaryBulletHandler3 = Instantiate(arrow, arrowFirePoints[5].transform.position, lookRotation);
                TemporaryBulletHandler3.GetComponent<Rigidbody2D>().velocity = (position - arrowFirePoints[5].transform.position).normalized * arrowForce;
            }
        }
        else
        {
            Quaternion direction = new Quaternion(cannonFirePoint.transform.rotation.x, cannonFirePoint.transform.rotation.y, cannonFirePoint.transform.rotation.z + Random.Range(-cannon_RandomDeviation, cannon_RandomDeviation), cannonFirePoint.transform.rotation.w);
            GameObject TemporaryBulletHandler = Instantiate(bullet, cannonFirePoint.transform.position, direction);
            TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.up * bulletForce;
        }
    }
}
