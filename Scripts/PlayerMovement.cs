using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 _movement;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    Camera cam;

    private bool canMove = true;
    
    void Start()
    {
        _movement = new Vector2();
        PlayerHealth health = GetComponent<PlayerHealth>();
        health.OnDeathEvent += StopMovement;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void UpdateMovementInput()
    {
        _movement.x = Input.GetKey(KeyCode.D) ? 1 : (Input.GetKey(KeyCode.A) ? -1 : 0);
        _movement.y = Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0);
    }


    private void Move()
    {
        if (!canMove)
        {
            return;
        }
        
        rb.MovePosition(rb.position + (_movement * (Time.fixedDeltaTime * movementSpeed)));
        
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
    }

    private void StopMovement()
    {
        canMove = false;
    }
}
