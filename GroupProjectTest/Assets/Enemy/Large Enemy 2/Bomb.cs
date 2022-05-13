using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float Time = 2f;
    public float damage = 20;
    public bool enemy_bullet;

    public float ExplosionRadius = 5;
    // Start is called before the first frame update
    public void Start()
    {
        if (enemy_bullet)
            Invoke("DamagePlayer", Time);
        else
            Invoke("DamageEnemy", Time);
    }

    public void DamageEnemy()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            if (Vector3.Distance(enemys[i].transform.position, transform.position) < ExplosionRadius)
            {
                enemys[i].GetComponent<Health>().Take_Off_Health(damage);
            }
        }
        Destroy(gameObject);
    }
    public void DamagePlayer()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(player.transform.position, transform.position) < ExplosionRadius)
        {
            //collision.gameObject.GetComponent<healthbar>().damage(damage);
        }
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !enemy_bullet)
        {
            // DEAL DAMAGE TO THE ENEMIES
            CancelInvoke();
            DamageEnemy();
        }
        if (collision.tag == "Player" && enemy_bullet)
        {
            //DEAL DAMAGE TO THE PLAYER
            CancelInvoke();
            DamagePlayer();
        }
        else if (collision.tag == "SETTLEMENT" && !enemy_bullet)
        {
            collision.gameObject.GetComponent<Island_Health>().addHealth((int)damage);
            CancelInvoke();
            DamageEnemy();
        }
        else if (collision.tag == "Walls")
        {
            CancelInvoke();
            DamagePlayer();
        }

    }
}
