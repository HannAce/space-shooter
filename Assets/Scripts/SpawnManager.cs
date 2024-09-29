using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameplayConfig gameplayConfig;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject powerupContainer;

    private float spawnEnemyDelay;
    private float minEnemySpawnRate;
    private float maxEnemySpawnRate;
    private float spawnPowerupDelay;
    private float minPowerupSpawnRate;
    private float maxPowerupSpawnRate;

    private void Start()
    {
        minEnemySpawnRate = gameplayConfig.MinEnemySpawnRate;
        maxEnemySpawnRate = gameplayConfig.MaxEnemySpawnRate;
        minPowerupSpawnRate = gameplayConfig.MinPowerupSpawnRate;
        maxPowerupSpawnRate = gameplayConfig.MaxPowerupSpawnRate;
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2f);
        while (Player.Instance != null)
        {
            SpawnEnemy();
            spawnEnemyDelay = Random.Range(minEnemySpawnRate, maxEnemySpawnRate);
            yield return new WaitForSeconds(spawnEnemyDelay);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-9.4f, 9.4f), 8f, 0);

        GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.Euler(new Vector3(0, 0, 180f)));
        newEnemy.transform.SetParent(enemyContainer.transform);
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (Player.Instance != null)
        {
            SpawnPowerup();
            spawnPowerupDelay = Random.Range(minPowerupSpawnRate, maxPowerupSpawnRate);
            yield return new WaitForSeconds(spawnPowerupDelay);
    }
}

    private void SpawnPowerup()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-9.4f, 9.4f), 8f, 0);
        int randomPowerup = Random.Range(0, powerups.Length);

        if (powerups[randomPowerup] == null)
        {
            return;
        }

        GameObject newPowerup = Instantiate(powerups[randomPowerup], randomSpawnPosition, Quaternion.identity);
        newPowerup.transform.SetParent(powerupContainer.transform);
    }
}
