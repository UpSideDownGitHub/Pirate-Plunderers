using UnityEngine;

public class collisioncontroller : MonoBehaviour
{
    public HealthbarController healthBar;

      void onCollisionEnter2D(Collision2D collision)
    {
<<<<<<< Updated upstream:GroupProjectTest/Assets/collisioncontroller.cs
        if (collision.gameObject.tag == "Bullet")
=======
        if (collision.gameObject.tag == "Enemytest")
>>>>>>> Stashed changes:GroupProjectTest/Assets/Scripts/collisioncontroller.cs
        {
            if (healthBar)
            {
<<<<<<< Updated upstream:GroupProjectTest/Assets/collisioncontroller.cs
                healthBar.onTakeDamage(10);
=======
                healthbar.onTakeDamage(0.10f);
>>>>>>> Stashed changes:GroupProjectTest/Assets/Scripts/collisioncontroller.cs
            }
        }
    }
}
