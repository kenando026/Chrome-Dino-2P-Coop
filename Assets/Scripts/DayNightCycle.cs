using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Transform celestialPivot;
    public Camera mainCamera;
    public float cycleDuration = 30f;

    public Color dayColor = new Color(0.65f, 0.85f, 1f);
    public Color nightColor = new Color(0.05f, 0.05f, 0.15f);

    private float t;

    void Update()
    {
        if (GameManager.Instance.State != GameManager.GameState.Playing) return;

        t += Time.deltaTime / cycleDuration;
        if (t > 1f) t -= 1f;

        if (celestialPivot)
            celestialPivot.rotation = Quaternion.Euler(0f, 0f, t * 360f);

        float dayAmount = (Mathf.Cos(t * Mathf.PI * 2f) + 1f) / 2f;
        if (mainCamera)
            mainCamera.backgroundColor = Color.Lerp(nightColor, dayColor, dayAmount);
    }
}