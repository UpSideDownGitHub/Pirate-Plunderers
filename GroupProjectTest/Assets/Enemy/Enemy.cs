using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Options")]
    public bool useCannon;
    public bool useArrows;
    public bool useSideCannons;
    public bool useBombs;
    public bool useRam;

    [Header("Nav Mesh")]
    public NavMeshAgent navMesh;
    public float speed;
    public float acceleration;

    public float maxdistacne;
    public float stoppingDistance;

    [Header("Distances")]
    public float maxCannonAttackDistance;
    public float minCannonAttackDistance;

    [Header("Attacking")]
    public GameObject Player;
    public bool attacking;
    public float attackTime;
    public float attackTime2;
    public float attackTime3;

    [Header("Rotation")]
    public float rotationSpeed;

    [Header("Seen Player")]
    public bool seenPlayer;

    [Header("Random Movement")]
    public float randomMovementRadius;
    bool doOnce = false;
    bool hasPath = false;

    [Header("Shooting")]
    [Header("Cannon")]
    public GameObject cannonFirePointMaster;
    public GameObject[] cannonFirePoint;
    public GameObject cannonBullet;
    public float cannonBulletForce;
    public float cannonRandomDeviation;
    public float cannonRotateSpeed;

    [Header("Arrow")]
    public bool arrows;
    public GameObject[] arrowFirePoints;
    public GameObject arrowBullet;
    public float arrowBulletForce;
    public float arrowRandomDeviation;

    [Header("Side Cannons")]
    public GameObject[] sideCannonFirePoints;
    public GameObject sideCannonBullet;
    public float sideCannonForce;
    public float sideCannonRandomDeviation;

    [Header("Bombs")]
    public GameObject bombFirePoint;
    public GameObject bombBullet;
    public float bombBulletForce;
    public float bombRandomDeviation;
    public float mortorRotateSpeed;

    [Header("Ram")]
    public float maxRamDistance;
    public float minRamDistance;
    public float ramSpeed;
    public float ramAcceleration;
    public float ramCoolDown;
    public float ramTime;
    public bool ram = false;
    public bool canRam = true;



    // Start is called before the first frame update
    void Start()
    {
        var agent = GetComponentInChildren<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        seenPlayer = false;

        doOnce = true;

        Player = GameObject.FindGameObjectWithTag("Player");

        navMesh.speed = speed;
        navMesh.acceleration = acceleration;
        navMesh.stoppingDistance = stoppingDistance;
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
            if (!doOnce)
                navMesh.ResetPath();
            doOnce = true;
            // SEEN THE PLAYER
            seenPlayer = true;

            // ROTATION
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, navMesh.velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // ROTATE THE CANNON TO FACE THE PLAYER
            if (useCannon)
            {
                Vector3 moveDirection = (Player.transform.position - cannonFirePointMaster.transform.position).normalized;
                Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, moveDirection);
                cannonFirePointMaster.transform.rotation = Quaternion.RotateTowards(cannonFirePointMaster.transform.rotation, toRotation2, cannonRotateSpeed * Time.deltaTime);
            }
            else if (useBombs)
            {
                Vector3 moveDirection = (Player.transform.position - bombFirePoint.transform.position).normalized;
                Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, moveDirection);
                bombFirePoint.transform.rotation = Quaternion.RotateTowards(bombFirePoint.transform.rotation, toRotation2, mortorRotateSpeed * Time.deltaTime);
            }

            // WHEN THE VELOCITY IS ZERO NEED TO KEEP ROTATION

            // in range of the player and can attack them
            if (distance <= maxCannonAttackDistance)
            {
                // ENABLE ARROWS
                if (useArrows)
                {
                    if (distance <= minCannonAttackDistance)
                        arrows = true;
                    else
                        arrows = false;
                }

                if (useRam)
                {
                    if (distance <= maxRamDistance && distance >= minRamDistance && !ram && canRam)
                        ram = true;
                }

                // if not allready attcking the player
                if (!attacking)
                {
                    // attack the player
                    InvokeRepeating("Attack", 0, attackTime);
                    if (useSideCannons)
                        InvokeRepeating("SideAttack", 0, attackTime2);
                    if (useBombs)
                        InvokeRepeating("BombAttack", 0, attackTime3);
                }
                attacking = true;
            }
            else
            {
                // if attacking the player and the ship is now out of range of the player
                if (attacking)
                {
                    // stop attacking the player
                    CancelInvoke("Attack");
                    if (useSideCannons)
                        CancelInvoke("SideAttack");
                    if (useBombs)
                        CancelInvoke("BombAttack");
                }
                attacking = false;
            }

            // set a new destination of the player
            navMesh.SetDestination(Player.transform.position);
            hasPath = false;
        }
        else
        {
            if (doOnce)
            {
                doOnce = false;
                //navMesh.ResetPath();
                CancelInvoke("Attack");
                if (useSideCannons)
                    CancelInvoke("SideAttack");
                if (useBombs)
                    CancelInvoke("BombAttack");
                attacking = false;
            }

            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, navMesh.velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);


            if (navMesh.remainingDistance <= navMesh.stoppingDistance + 1f)
            {
                Vector2 point;
                do
                {
                    point = Random.insideUnitCircle * randomMovementRadius;
                    point += (Vector2)transform.position;
                } while (!NavMesh.SamplePosition(new Vector3(point.x, point.y, 0), out _, 0.1f, NavMesh.AllAreas));

                navMesh.SetDestination(point);
                hasPath = false;
            }
        }
    }

    // the attack function of the ship
    public void Attack()
    {
        if (useCannon && !arrows)
        {
            for (int i = 0; i < cannonFirePoint.Length; i++)
            {
                Quaternion direction = new Quaternion(cannonFirePoint[i].transform.rotation.x, cannonFirePoint[i].transform.rotation.y, cannonFirePoint[i].transform.rotation.z + Random.Range(-cannonRandomDeviation, cannonRandomDeviation), cannonFirePoint[i].transform.rotation.w);
                GameObject TemporaryBulletHandler = Instantiate(cannonBullet, cannonFirePoint[i].transform.position, direction);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.up * cannonBulletForce;
            }
        }

        if (useArrows && arrows)
        {
            int i = 3;
            if ((arrowFirePoints[1].transform.position - Player.transform.position).sqrMagnitude < (arrowFirePoints[4].transform.position - Player.transform.position).sqrMagnitude)
                i = 0;

            int total = i + arrowFirePoints.Length / 2;
            for (; i < total; i++)
            {
                float ran = Random.Range(-arrowRandomDeviation, arrowRandomDeviation);
                Vector3 position = Player.transform.position;
                position.x += ran;
                position.y += ran;

                Vector3 vector = (position - arrowFirePoints[i].transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, vector);
                GameObject TemporaryBulletHandler = Instantiate(arrowBullet, arrowFirePoints[i].transform.position, lookRotation);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = (position - arrowFirePoints[i].transform.position).normalized * arrowBulletForce;
            }
        }

        if (useRam)
        {
            if (ram)
                StartCoroutine(ramAttack());
        }
    }

    public void BombAttack()
    {
        Quaternion direction = new Quaternion(bombFirePoint.transform.rotation.x, bombFirePoint.transform.rotation.y, bombFirePoint.transform.rotation.z + Random.Range(-bombRandomDeviation, bombRandomDeviation), bombFirePoint.transform.rotation.w);
        GameObject TemporaryBulletHandler = Instantiate(bombBullet, bombFirePoint.transform.position, direction);
        TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.up * bombBulletForce;
    }

    public void SideAttack()
    {
        if ((sideCannonFirePoints[1].transform.position - Player.transform.position).sqrMagnitude < (sideCannonFirePoints[5].transform.position - Player.transform.position).sqrMagnitude)
        {
            for (int i = 0; i < 4; i++)
            {
                Quaternion direction = new Quaternion(sideCannonFirePoints[i].transform.rotation.x, sideCannonFirePoints[i].transform.rotation.y, sideCannonFirePoints[1].transform.rotation.z + Random.Range(-sideCannonRandomDeviation, sideCannonRandomDeviation), sideCannonFirePoints[1].transform.rotation.w);
                GameObject TemporaryBulletHandler = Instantiate(sideCannonBullet, sideCannonFirePoints[i].transform.position, direction);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = -TemporaryBulletHandler.transform.right * sideCannonForce;
            }
        }
        else
        {
            for (int i = 4; i < 8; i++)
            {
                Quaternion direction = new Quaternion(sideCannonFirePoints[i].transform.rotation.x, sideCannonFirePoints[i].transform.rotation.y, sideCannonFirePoints[1].transform.rotation.z + Random.Range(-sideCannonRandomDeviation, sideCannonRandomDeviation), sideCannonFirePoints[1].transform.rotation.w);
                GameObject TemporaryBulletHandler = Instantiate(sideCannonBullet, sideCannonFirePoints[i].transform.position, direction);
                TemporaryBulletHandler.GetComponent<Rigidbody2D>().velocity = TemporaryBulletHandler.transform.right * sideCannonForce;
            }
        }
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

    public IEnumerator ramAttack()
    {
        ram = true;
        canRam = false;
        navMesh.speed = ramSpeed;
        navMesh.acceleration = ramAcceleration;
        yield return new WaitForSeconds(ramTime);
        navMesh.speed = speed;
        navMesh.acceleration = acceleration;
        ram = false;
        yield return new WaitForSeconds(ramCoolDown);
        canRam = true;
    }
}


// TO DRAW THE PATH THAT IS CURRENTLY BEING FOLLOWED
/*
var path = NavMesh.path;
for (int i = 0; i < path.corners.Length - 1; i++)
{ 
    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
}
*/