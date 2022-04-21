//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    public Image healthBar;
    public float health;
    public float starthealth;

   public void onTakeDamage(int damage)
{
    health = health - damage;
    healthBar.fillAmount = health / starthealth;
}
}
