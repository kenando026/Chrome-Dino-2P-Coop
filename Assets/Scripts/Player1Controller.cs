using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    // Sesuaikan nilai ini langsung di Inspector Unity jika kurang pas
    public float minX = -6.8f; 
    public float maxX = 8.5f;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection = 0f;
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = -1f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Jalankan pergerakan fisik
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        // Kunci posisi X agar tidak pernah melewati minX dan maxX
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true; 

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Object.FindFirstObjectByType<GameManager>().GameOver(); 
        }
    }
}