using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float destroyX = -15f;

    void Update()
    {
        if (GameManager.Instance.State != GameManager.GameState.Playing) return;

        transform.Translate(Vector2.left * GameManager.Instance.CurrentSpeed * Time.deltaTime);

        if (transform.position.x < destroyX) Destroy(gameObject);
    }
}