using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject tripleShotPowerUpPrefab;
    [SerializeField]
    private GameObject SpeedPowerUpPrefab;
    [SerializeField]
    private GameObject shieldPowerUpPrefab;

    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject powerUpContainer;

    [SerializeField]
    private float spawnDelay;
    private float countDownTimer;

    void Update()
    {
        if (Player.Instance == null)
        {
            countDownTimer = 0;
            return;
        }

        if (countDownTimer <= 0)
        {
            SpawnEnemy();
            spawnDelay = Random.Range(2f, 5f);
            countDownTimer = spawnDelay;
        }
        else
        {
            countDownTimer -= Time.deltaTime;
        }
    }

 /*   IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }*/

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);

        GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
        newEnemy.transform.SetParent(enemyContainer.transform);
    }
}
