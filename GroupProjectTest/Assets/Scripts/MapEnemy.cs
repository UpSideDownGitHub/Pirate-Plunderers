using UnityEngine;

public class MapEnemy : MonoBehaviour{

    public Transform player;
    public float movespeed = 5f;
    public float rotationspeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        
    }

    public void FixedUpdate(){
        moveCharacter(movement);
    }

      void moveCharacter(Vector2 direction) {
          rb.MovePosition((Vector2)transform.position + (direction * rotationspeed * movespeed * Time.deltaTime));

    }
}

