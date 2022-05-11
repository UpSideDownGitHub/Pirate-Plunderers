//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Slider healthbarslider;

    public float maxHealth;
    public float currenthealth;

    public GameObject Deathcanvus;
    public ParticleSystem Playerdeath;

    bool doOnce = true;


    private void Start()
    {
        currenthealth = maxHealth;
        healthbarslider.minValue = 0;
        healthbarslider.maxValue = maxHealth;
        healthbarslider.value = currenthealth;
        Deathcanvus.SetActive(false);
    }

    public void damage(float damage)
    {
        currenthealth -= damage;
        healthbarslider.value = currenthealth;
    }

    void Update()
    {
        if (currenthealth <= 0 && doOnce)
        {
            doOnce = false;
            Instantiate(Playerdeath, transform.position, Quaternion.identity);
            Invoke ("delay", 2f);
        }
    }
    public void delay()
    {

        Destroy(gameObject);
        Deathcanvus.SetActive(true);
        Time.timeScale = 0;
    }
}
 
 


