using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float destroyBoundaryX = -12f;

    void Update()
    {
        // Geser ke kiri
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Hapus jika sudah keluar dari layar kiri
        if (transform.position.x <= destroyBoundaryX)
        {
            Destroy(gameObject);
        }
    }
}