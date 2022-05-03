using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float Time;

    public void Start()
    {
        Destroy(gameObject, Time);
    }
}
