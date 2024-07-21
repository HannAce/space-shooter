using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player player = Player.Instance;

    [SerializeField]
    private float enemySpeed = 2f;
  
    void Update()
    {
        EnemyMovement();
    }

    // Moves enemy downwards while player is alive, and respawns at a random position at the top of the screen if it goes off the bottom
    private void EnemyMovement()
    {
        float randomPositionX = Random.Range(-9.4f, 9.4f);

        transform.Translate(Vector3.down * Time.deltaTime * enemySpeed);

        if (transform.position.y < -6.1f && player != null)
        {
            transform.position = new Vector3(randomPositionX, 7.5f, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckPlayerCollision(other);

        // Checks whether enemy colliding with a fired weapon, if so destroys both enemy and weapon
        if (other.gameObject.tag == "Weapon")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    // Checks whether enemy colliding with player, and if so causes player damage then destroys enemy
    private void CheckPlayerCollision(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        if (player == null)
        {
            Debug.LogError("Enemy Script: Player reference is null.");
            return;
        }

        player.TakeDamage(1);
        Destroy(this.gameObject);
    }
}