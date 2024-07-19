using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed = 2f;
  
    void Update()
    {
        EnemyMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks whether enemy colliding with player, and if so causes player damage then destroys enemy
        if (other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogWarning("Enemy Script: Player reference is null.");
            }
        }

        // Checks whether enemy colliding with a fired weapon, if so destroys both enemy and weapon
        if (other.gameObject.tag == "Weapon")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
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