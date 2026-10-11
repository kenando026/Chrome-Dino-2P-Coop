using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    public Transform otherGround;
    public float groundWidth = 30f;
    
    [Tooltip("Nilai overlap untuk menutup celah/undakan piksel di ujung sprite")]
    public float overlap = 0.1f; 

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        // Simpan koordinat Y dan Z awal agar kuncinya 100% konsisten sejak game mulai
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void Update()
    {
        // 1. Geser tanah ke kiri menggunakan pos X langsung (presisi tinggi)
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        pos.y = fixedY; // Kunci Y agar tidak pernah bergeser berundak
        pos.z = fixedZ;
        transform.position = pos;

        // 2. Teleport tanah saat sudah melewati batas lebar tanahnya
        if (transform.position.x <= -groundWidth)
        {
            // Pindahkan tepat ke belakang otherGround dan kurangi nilai overlap
            pos.x = (otherGround.position.x + groundWidth) - overlap;
            pos.y = otherGround.position.y;
            pos.z = otherGround.position.z;

            transform.position = pos;
        }
    }
}