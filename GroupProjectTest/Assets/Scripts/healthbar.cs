//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Slider healthbarslider;

    public float maxHealth;
    public static float currenthealth;

    public GameObject Deathcanvus;


    private void Start()
{
    currenthealth = maxHealth;
    Deathcanvus.SetActive(false);
}

    void Update()
    {
        healthbarslider.value = currenthealth;

        if (currenthealth <= 0)
        {
            Deathcanvus.SetActive(true);
            Time.timeScale = 0;

        }
}
}
