using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Image livesImage;
    [SerializeField] private Sprite[] livesSprites;

    Player player;
    GameManager gameManager;

    void Start()
    {
        player = Player.Instance;
        gameManager = GameManager.Instance;

        player.OnLivesUpdated += LivesUpdated;

        player.OnScoreUpdated += ScoreUpdated;
        scoreText.text = "Score: " + 0;

        gameManager.OnHighScoreUpdated += HighScoreUpdated;
        highScoreText.text = "High Score: " + gameManager.HighScore;
    }

    private void OnDestroy()
    {
        if (Player.Instance != null)
        {
            player.OnScoreUpdated -= ScoreUpdated;
            player.OnLivesUpdated -= LivesUpdated;
        }

        if (gameManager != null)
        {
            gameManager.OnHighScoreUpdated -= HighScoreUpdated;
        }
    }

    private void LivesUpdated(int lives)
    {
        livesImage.sprite = livesSprites[lives];
        Debug.Log("Lives remaining: " + lives);
    }

    private void ScoreUpdated(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    private void HighScoreUpdated(int newHighScore)
    {
        highScoreText.text = "High Score: " + newHighScore;
    }
}
