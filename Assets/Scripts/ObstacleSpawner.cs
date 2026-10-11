using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float minInterval = 0.8f;
    public float maxInterval = 1.8f;

    private float timer;

    void Start() { timer = Random.Range(minInterval, maxInterval); }

    void Update()
    {
        if (GameManager.Instance.State != GameManager.GameState.Playing) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            int i = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[i], transform.position, Quaternion.identity);
            timer = Random.Range(minInterval, maxInterval);
        }
    }
}