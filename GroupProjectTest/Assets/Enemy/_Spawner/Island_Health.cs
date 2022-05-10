using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Island_Health : MonoBehaviour
{
    [Header("Objects")]
    public Slider healthBar;
    public GameObject healthBarCanvas;
    public GameObject enemySpawner;

    [Header("Variables")]
    public int health;
    public float decreaseTime;
    public float decreaseAmmount;

    [Header("Bools")]
    public bool canHaveHealthBar = true;
    
    bool doOnce = true;


    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<Slider>();
        healthBar.value = 0;
        healthBarCanvas.SetActive(false);   
    }

    public void DecreaseValue()
    {
        healthBar.value -= decreaseAmmount;
    }

    public void addHealth(int ammount)
    {
        healthBar.value += ammount;

        if (healthBar.value >= healthBar.maxValue && canHaveHealthBar)
        {
            // START THE ENEMY ONSLAUGHT AS HAS AWOKEN THE ISLAND
            enemySpawner.SetActive(true);
            canHaveHealthBar = false;
            doOnce = true;
            healthBarCanvas.SetActive(false);
            CancelInvoke("DecreaseValue");
            healthBar.value = 0;
        }
    }

    void Update()
    {
        if (canHaveHealthBar)
        {
            if (healthBar.value <= 0)
            {
                doOnce = true;
                healthBarCanvas.SetActive(false);
                CancelInvoke("DecreaseValue");
                healthBar.value = 0;
            }
            else if (doOnce)
            {
                doOnce = false;
                InvokeRepeating("DecreaseValue", decreaseTime, decreaseTime);
                healthBarCanvas.SetActive(true);
            }
        }
    }
}
