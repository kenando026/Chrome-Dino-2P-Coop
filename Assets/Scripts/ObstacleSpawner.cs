using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float spawnPositionY = -3.1f;
    public float spawnPositionX = 5f; // Diturunkan ke 5f agar PASTI masuk layar kamera

    private float timer;

    void Start()
    {
        Debug.Log("Spawner Aktif!");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        if (obstaclePrefab == null)
        {
            Debug.LogError("Obstacle Prefab MASIH KOSONG di Inspector!");
            return;
        }

        // Spawn di koordinat X=5 (tengah-kanan layar)
        Vector3 spawnPos = new Vector3(spawnPositionX, spawnPositionY, 0f);
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // Paksa Scale & Order in Layer
        newObstacle.transform.localScale = Vector3.one;

        SpriteRenderer sr = newObstacle.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = 20; // Layer sangat tinggi agar tidak tertutup apapun
        }

        Debug.Log("Kaktus Berhasil Di-spawn di X: " + spawnPos.x);
    }
}