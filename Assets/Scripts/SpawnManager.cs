using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {

            Vector3 randomSpawnPosition = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);

            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
