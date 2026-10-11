using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveDirection = 0f;
        
        // Logika Bergerak Kiri & Kanan (Panah Kiri dan Kanan)
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = -1f;
        }

        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        // Logika Melompat (Panah Atas)
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
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