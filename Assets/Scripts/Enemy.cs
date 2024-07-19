using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

    // Moves enemy downwards, and respawns at a random position at the top of the screen if it goes off the bottom
    private void EnemyMovement()
    {
        float randomPositionX = Random.Range(-9.4f, 9.4f);

        transform.Translate(Vector3.down * Time.deltaTime * enemySpeed);

        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(randomPositionX, 7.5f, transform.position.z);
        }
    }
}