using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int Max_Health;
    public float Current_Health;

    // Start is called before the first frame update
    public void Start()
    {
        Current_Health = Max_Health;
    }

    public void Take_Off_Health(float damage)
    {
        Current_Health -= damage;
        if (Current_Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
