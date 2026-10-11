using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public enum PlayerNumber { Player1, Player2 }
    public PlayerNumber playerNumber = PlayerNumber.Player1;

    [Header("Gerakan")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Menunduk")]
    public float duckScaleY = 0.5f;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isGrounded;
    private bool isAlive = true;
    private Vector3 normalScale;

    private KeyCode jumpKey, duckKey, leftKey, rightKey;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        normalScale = transform.localScale;

        if (playerNumber == PlayerNumber.Player1)
        {
            jumpKey = KeyCode.W; duckKey = KeyCode.S;
            leftKey = KeyCode.A; rightKey = KeyCode.D;
        }
        else
        {
            jumpKey = KeyCode.UpArrow; duckKey = KeyCode.DownArrow;
            leftKey = KeyCode.LeftArrow; rightKey = KeyCode.RightArrow;
        }

        // Paksa status awal di tanah agar bisa langsung lompat saat game mulai
        isGrounded = true;
    }

    void Update()
    {
        if (!isAlive) return;

        // Cek deteksi tanah
        CekTanah();

        // Hentikan gerakan jika game belum running/play
        if (GameManager.Instance != null && GameManager.Instance.State != GameManager.GameState.Playing) return;

        // Gerak kiri/kanan
        float h = 0;
        if (Input.GetKey(leftKey)) h = -1;
        if (Input.GetKey(rightKey)) h = 1;
        rb.linearVelocity = new Vector2(h * moveSpeed, rb.linearVelocity.y);

        // Lompat langsung responsif
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        // Menunduk
        if (Input.GetKey(duckKey))
        {
            if (isGrounded)
                transform.localScale = new Vector3(normalScale.x, normalScale.y * duckScaleY, normalScale.z);
            else
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -jumpForce);
        }
        else
        {
            transform.localScale = normalScale;
        }
    }

    void CekTanah()
    {
        if (col == null) return;

        // Area deteksi tanah di telapak kaki
        Bounds bounds = col.bounds;
        Vector2 posKaki = new Vector2(bounds.center.x, bounds.min.y);

        // Deteksi area di bawah kaki dengan OverlapBox
        Collider2D hit = Physics2D.OverlapBox(posKaki, new Vector2(bounds.size.x * 0.9f, 0.25f), 0f);

        if (hit != null && hit.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && isAlive) Die();
    }

    void Die()
    {
        // Cari SEMUA player di dalam game, lalu matikan semuanya bersamaan!
        PlayerController[] allPlayers = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
        foreach (PlayerController player in allPlayers)
        {
            player.isAlive = false;
            player.rb.linearVelocity = Vector2.zero;
            player.rb.simulated = false; // Player berhenti bergerak
            player.GetComponent<SpriteRenderer>().color = Color.red; // Beri warna merah saat mati
        }

        // Panggil fungsi PlayerDied di GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerDied();
        }
    }
}