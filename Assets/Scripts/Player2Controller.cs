using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    // Ubah nilai minX di Inspector menjadi -6.8f agar pas di pinggir layar
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
        
        // Logika Bergerak Kiri & Kanan (Tombol A dan D)
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1f;
        }

        // Logika Melompat (Tombol W)
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Pergerakan fisik berjalan di FixedUpdate
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        // Kunci posisi X agar Player2 tidak tembus layar kiri/kanan
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