using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    
    [Tooltip("Lebar visual 1 objek tanah. Sesuaikan angkanya di Inspector jika masih ada celah.")]
    public float groundWidth = 20f; 

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        // Simpan koordinat Y dan Z awal agar tanah tidak pernah bergeser naik/turun
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void Update()
    {
        // 1. Geser tanah ke kiri
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        pos.y = fixedY; // Kunci Y agar tidak naik-turun
        pos.z = fixedZ;
        transform.position = pos;

        // 2. Lempar balik ke belakang tanah sebelahnya saat sudah melampaui groundWidth
        if (transform.position.x <= -groundWidth)
        {
            // Tambahkan (groundWidth * 2) ke posisi X saat ini agar menyambung presisi
            Vector3 resetPosition = new Vector3(transform.position.x + (groundWidth * 2f), fixedY, fixedZ);
            transform.position = resetPosition;
        }
    }
}