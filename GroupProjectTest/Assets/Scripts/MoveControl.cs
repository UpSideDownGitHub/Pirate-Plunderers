using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    [SerializeField]
    private GameObject circle, dot;

    private Rigidbody2D rb;

    private float moveSpeed;

    private Touch oneTouch;

    private Vector2 touchPosition;

    private Vector2 moveDirection;

    public float moveLimiter = 0.7f;

    public float rotationSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circle.SetActive(false);
        dot.SetActive(false);
        moveSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            oneTouch = Input.GetTouch(0);

            touchPosition = Camera.main.ScreenToWorldPoint(oneTouch.position);
            switch(oneTouch.phase)
            {
                case TouchPhase.Began:
                    circle.SetActive(true);
                    dot.SetActive(true);
                    circle.transform.position = touchPosition;
                    dot.transform.position = touchPosition;
                    break;
                case TouchPhase.Stationary:
                    MoveShip();
                    break;
                case TouchPhase.Moved:
                    MoveShip();
                    break;
                case TouchPhase.Ended:
                    rb.velocity = new Vector2(0, 0);
                    circle.SetActive(false);
                    dot.SetActive(false);
                    break;
            }
        }
    }

    private void MoveShip()
    {
        dot.transform.position = touchPosition;

        dot.transform.position = new Vector2(
            Mathf.Clamp(dot.transform.position.x,
            circle.transform.position.x - 1f,
            circle.transform.position.x + 1f),
            Mathf.Clamp(dot.transform.position.y,
            circle.transform.position.y - 1f,
            circle.transform.position.y + 1f));

        moveDirection = (dot.transform.position - circle.transform.position).normalized;

        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);


        rb.velocity = moveDirection * moveSpeed;

        /*
        moveDirection = new Vector2(Mathf.Round(moveDirection.x), Mathf.Round(moveDirection.y));
       
        // LEFT
        if (moveDirection.x == -1 && moveDirection.y == 0)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        // RIGHT
        else if (moveDirection.x == 1 && moveDirection.y == 0)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        // UP
        else if (moveDirection.x == 0 && moveDirection.y == 1)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        // DOWN
        else if (moveDirection.x == 0 && moveDirection.y == -1)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        // LEFT-UP
        else if (moveDirection.x == -1 && moveDirection.y == 1)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 45));
        // LEFT-DOWN
        else if (moveDirection.x == -1 && moveDirection.y == -1)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 135));
        // RIGHT-UP
        else if (moveDirection.x == 1 && moveDirection.y == 1)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -45));
        // RIGHT-DOWN
        else if (moveDirection.x == 1 && moveDirection.y == -1)
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -135));

        if (moveDirection.x != 0 && moveDirection.y != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            moveDirection *= moveLimiter;
        }
        */

    }
}
