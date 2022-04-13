using UnityEngine;

public class collisioncontroller : MonoBehaviour
{
    public HealthbarController healthbar;

    void onCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (healthbar)
            {
                healthbar.onTakeDamage(10);
            }
        }
    }
}
