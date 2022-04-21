using UnityEngine;

public class collisioncontroller : MonoBehaviour
{
    public HealthbarController healthBar;

    void onCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (healthBar)
            {
                healthBar.onTakeDamage(10);
            }
        }
    }
}
