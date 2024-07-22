using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Player.Instance.OnDeath += StartGameOver;
    }

    private void OnDestroy()
    {
        if (Player.Instance != null)
        {
            Player.Instance.OnDeath -= StartGameOver;
        }
    }

    private void StartGameOver()
    {
        StartCoroutine(StartGameOverRoutine());
    }

    IEnumerator StartGameOverRoutine()
    {
        Debug.Log("GAME OVER! Game will restart.");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
