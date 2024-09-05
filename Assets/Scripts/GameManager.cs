using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const int MainMenu = 0;
    public const int GameScene = 1;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject restartText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;

    //public int HighScore
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetInt("HighScore", 0);
    //    }
    //}

    public int HighScore => PlayerPrefs.GetInt("HighScore", 0);

    bool isGameOver = false;

    public Action<int> OnHighScoreUpdated;

    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        gameOverText.SetActive(false);
        restartText.SetActive(false);
        newHighScoreText.gameObject.SetActive(false);
        AudioManager.Instance.ToggleMusic(true);

        Player.Instance.OnDeath += StartGameOver;
    }

    private void OnDestroy()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMusic(false);
        }

        if (Player.Instance != null)
        {
            Player.Instance.OnDeath -= StartGameOver;
        }

        Instance = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isGameOver)
        {
            PauseGame();
            if (optionsMenu.activeSelf == true)
            {
                optionsMenu.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMainMenu();
        }

                if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            isGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void PauseGame()
    {
        if (pauseMenu.activeSelf == false)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Debug.Log("Game Paused");
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            Debug.Log("Game Playing");
        }
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(MainMenu);
    }

    private void StartGameOver()
    {
        isGameOver = true;

        StartCoroutine(StartGameOverRoutine());
        restartText.SetActive(true);
        TrySetHighScore(Player.Instance.Score);
    }

    IEnumerator StartGameOverRoutine()
    {
        while (isGameOver)
        {
            gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }

    public void TrySetHighScore(int score, bool ignoreScoreCheck = false)
    {
        // if ignoreScoreCheck is true, don't run the score check/return
        if (!ignoreScoreCheck && score <= HighScore)
        {
            return;
        }

        if (!ignoreScoreCheck)
        {
            newHighScoreText.text = "NEW HIGH SCORE!: " + score;
            newHighScoreText.gameObject.SetActive(true);
        }

        PlayerPrefs.SetInt("HighScore", score);
        OnHighScoreUpdated?.Invoke(score);
    }
}
