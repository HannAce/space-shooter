using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnScoreUpdated += ScoreUpdated;
        scoreText.text = "Score: " + 0;
    }

    private void OnDestroy()
    {
        if (Player.Instance != null)
        {
            Player.Instance.OnScoreUpdated -= ScoreUpdated;
        }
    }

    private void ScoreUpdated(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
