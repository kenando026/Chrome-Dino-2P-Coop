using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Kecepatan Game")]
    public float startSpeed = 5f;
    public float maxSpeed = 20f;
    public float speedIncrease = 0.1f;
    public float CurrentSpeed { get; private set; }

    [Header("Skor")]
    public float scoreMultiplier = 10f;
    public float Score { get; private set; }
    private int highScore;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public Button restartButton;

    public enum GameState { Playing, GameOver }
    public GameState State { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        CurrentSpeed = startSpeed;
        Score = 0;
        State = GameState.Playing;
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Pastikan pop-up tersembunyi di awal game
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (restartButton) restartButton.onClick.AddListener(RestartGame);

        UpdateUI();
    }

    void Update()
    {
        if (State == GameState.Playing)
        {
            // Kecepatan bertambah seiring waktu, skor bertambah tanpa batas (Endless)
            CurrentSpeed = Mathf.Min(CurrentSpeed + speedIncrease * Time.deltaTime, maxSpeed);
            Score += CurrentSpeed * scoreMultiplier * Time.deltaTime;
            UpdateUI();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + (int)Score;
        if (highScoreText) highScoreText.text = "HI: " + highScore;
    }

    // Dipanggil saat ada pemain yang menabrak kaktus
    public void PlayerDied()
    {
        if (State == GameState.GameOver) return;

        State = GameState.GameOver;
        SaveHighScore();
        
        // Tampilkan pop-up restart
        if (gameOverText) gameOverText.text = "GAME OVER";
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }

    void SaveHighScore()
    {
        if ((int)Score > highScore)
        {
            highScore = (int)Score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        UpdateUI();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}