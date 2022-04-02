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
        Invoke("DamagePlayer", Time);
    }

    public void DamagePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(player.transform.position, transform.position) < ExplosionRadius)
        {
            Debug.Log("Damege Player");
        }
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !enemy_bullet)
        {
            collision.gameObject.GetComponent<Health>().Take_Off_Health(damage);
            Destroy(gameObject);
        }
        if (collision.tag == "Player" && enemy_bullet)
        {
            //DEAL DAMAGE TO THE PLAYER
            CancelInvoke();
            DamagePlayer();
        }
        else if (collision.tag == "Walls")
            DamagePlayer();
    }
}
