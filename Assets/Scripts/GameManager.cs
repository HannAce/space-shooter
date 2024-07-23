using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverText;
    [SerializeField]
    GameObject restartText;

    bool isGameOver = false;

    void Start()
    {
        gameOverText.SetActive(false);
        restartText.SetActive(false);

        Player.Instance.OnDeath += StartGameOver;
    }

    private void OnDestroy()
    {

        if (Player.Instance != null)
        {
            Player.Instance.OnDeath -= StartGameOver;
        }
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            isGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void StartGameOver()
    {
        isGameOver = true;

        StartCoroutine(StartGameOverRoutine());
        restartText.SetActive(true);
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
}
