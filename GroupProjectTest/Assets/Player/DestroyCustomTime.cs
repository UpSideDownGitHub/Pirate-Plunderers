using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCustomTime : MonoBehaviour
{
    public float Time = 2f;
    public float damage = 20;
    public bool enemy_bullet;
    // Start is called before the first frame update
    public void Start()
    {
        Destroy(gameObject, Time);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && !enemy_bullet)
        {
            collision.collider.GetComponent<Health>().Take_Off_Health(damage);
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Walls")
            Destroy(gameObject);

    }
}
