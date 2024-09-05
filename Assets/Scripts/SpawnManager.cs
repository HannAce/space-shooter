using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject powerupContainer;

    private float spawnEnemyDelay;
    private float spawnPowerupDelay;

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
            spawnEnemyDelay = Random.Range(0.3f, 3f);
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
            spawnPowerupDelay = Random.Range(6f, 8f);
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
