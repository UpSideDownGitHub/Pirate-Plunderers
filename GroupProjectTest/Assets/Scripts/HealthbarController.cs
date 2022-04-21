using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    public Image healthBar;
    public float health;
    public float starthealth;

<<<<<<< Updated upstream
   public void onTakeDamage(int damage)
=======
   public void onTakeDamage(float damage)
>>>>>>> Stashed changes
{
    health = health - damage;
    healthBar.fillAmount = health / starthealth;
}
}
