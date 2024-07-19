using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private float spawnDelay = 5f;
    private float timer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance == null)
        {
            timer = 0;
            return;
        }

        if (timer <= 0)
        {
            SpawnEnemy();
            timer = spawnDelay;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);

        Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
    }
}
