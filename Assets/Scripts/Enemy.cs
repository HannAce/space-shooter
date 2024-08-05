using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player player;

    Animator animator;

    [SerializeField]
    GameObject enemyLaser;

    [SerializeField]
    private float enemySpeed = 2f;

    private bool isDamaging;

    private void Start()
    {
        player = Player.Instance;

        animator = GetComponent<Animator>();
        if (animator == null )
        {
            Debug.LogError("Enemy: Animator reference is null.");
        }

        isDamaging = true;

        StartCoroutine(EnemyShootRoutine());
    }

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

    IEnumerator EnemyShootRoutine()
    {
        float enemyShootDelay = Random.Range(1f, 5f);
        yield return new WaitForSeconds(enemyShootDelay);
        Instantiate(enemyLaser, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDamaging)
        {
            return;
        }
        CheckPlayerCollision(other);

        // Checks whether enemy colliding with a fired weapon, if so destroys both enemy and weapon
        if (other.gameObject.tag == "Weapon")
        {
            Destroy(other.gameObject);
            player.AddScore();
            DestroyEnemy();
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
            Debug.LogError("Enemy.CheckPlayerCollision(): Player reference is null.");
            return;
        }

        player.TakeDamage(1);
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        isDamaging = false;
        enemySpeed = 0f;
        animator.SetTrigger("OnEnemyDeath");
        AudioManager.Instance.PlayAudio(AudioType.Explosion);
        Destroy(this.gameObject, 1.5f);
    }
}