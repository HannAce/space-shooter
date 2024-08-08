using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Image livesImage;
    [SerializeField] private Sprite[] livesSprites;

    void Start()
    {
        Player.Instance.OnLivesUpdated += LivesUpdated;
        Player.Instance.OnScoreUpdated += ScoreUpdated;
        scoreText.text = "Score: " + 0;
    }

    private void OnDestroy()
    {
        if (Player.Instance != null)
        {
            Player.Instance.OnScoreUpdated -= ScoreUpdated;
            Player.Instance.OnLivesUpdated -= LivesUpdated;
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
}
