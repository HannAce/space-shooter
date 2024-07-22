using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject tripleShotPowerupPrefab;
    [SerializeField]
    private GameObject SpeedPowerupPrefab;
    [SerializeField]
    private GameObject shieldPowerupPrefab;

    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject powerupContainer;

    private float spawnEnemyDelay;
    private float spawnPowerupDelay;
    //private float countDownTimer;

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    void Update()
    {
        /*if (Player.Instance == null)
        {
            countDownTimer = 0;
            return;
        }

        if (countDownTimer <= 0)
        {
            SpawnEnemy();
            spawnEnemyDelay = Random.Range(2f, 5f);
            countDownTimer = spawnEnemyDelay;
        }
        else
        {
            countDownTimer -= Time.deltaTime;
        }*/
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (Player.Instance != null)
        {
            SpawnEnemy();
            spawnEnemyDelay = Random.Range(0.5f, 4f);
            yield return new WaitForSeconds(spawnEnemyDelay);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);

        GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        newEnemy.transform.SetParent(enemyContainer.transform);
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (Player.Instance != null)
        {
            SpawnPowerup();
            spawnPowerupDelay = Random.Range(6f, 8f);
            yield return new WaitForSeconds(spawnPowerupDelay);
    }
}

    private void SpawnPowerup()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);

        GameObject newPowerup = Instantiate(SpeedPowerupPrefab, randomSpawnPosition, Quaternion.identity);
        newPowerup.transform.SetParent(powerupContainer.transform);
    }
}
