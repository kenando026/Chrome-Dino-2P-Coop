using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public Transform groundA;
    public Transform groundB;

    private float width;

    void Start()
    {
        // Mengambil ukuran panjang asli tanah dari nilai Scale X di Inspector
        if (groundA != null)
        {
            width = groundA.localScale.x;
        }
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.State != GameManager.GameState.Playing) return;

        float move = GameManager.Instance.CurrentSpeed * Time.deltaTime;
        groundA.position += Vector3.left * move;
        groundB.position += Vector3.left * move;

        // Pindahkan tanah A tepat di belakang tanah B saat keluar layar
        if (groundA.position.x <= -width)
        {
            groundA.position = new Vector3(groundB.position.x + width, groundA.position.y, groundA.position.z);
        }

        // Pindahkan tanah B tepat di belakang tanah A saat keluar layar
        if (groundB.position.x <= -width)
        {
            groundB.position = new Vector3(groundA.position.x + width, groundB.position.y, groundB.position.z);
        }
    }
}