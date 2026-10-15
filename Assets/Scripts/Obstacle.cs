using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Movement Settings")]
    public float fallbackSpeed = 5f; // Kecepatan cadangan jika GameManager tidak ditemukan
    public float destroyBoundaryX = -12f;

    void Update()
    {
        // Ambil kecepatan dinamis dari GameManager jika ada, jika tidak pakai fallbackSpeed
        float currentSpeed = (GameManager.Instance != null) ? GameManager.Instance.gameSpeed : fallbackSpeed;

        // Geser ke kiri menggunakan kecepatan dinamis
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);

        // Hapus jika sudah melewati batas kiri layar
        if (transform.position.x <= destroyBoundaryX)
        {
            Destroy(gameObject);
        }
    }
}