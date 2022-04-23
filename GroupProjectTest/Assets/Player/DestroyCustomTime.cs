using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCustomTime : MonoBehaviour
{
    public float Time = 2f;
    public float damage = 20;
    public bool enemy_bullet;

    //healthbardamage damage;

    // Start is called before the first frame update
    public void Start()
    {
        Destroy(gameObject, Time);
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
            //damage
            //DEAL DAMAGE TO THE PLAYER
            Destroy(gameObject);
        }
        else if (collision.tag == "Walls")
        {
            try
            {
                collision.gameObject.GetComponent<Island_Health>().addHealth((int)damage/2);

                Destroy(gameObject);
            }
            catch
            {
                Destroy(gameObject);
            }
            
        }
    }
}
