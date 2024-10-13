using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameplayConfig gameplayConfig;

    private const float minFireRate = 3f;
    private const float maxFireRate = 6f;

    [SerializeField] GameObject enemyLaserPrefab;

    [SerializeField] Vector3 laserOffset;

    private float enemySpeed = 2f;
    private float fireRate = 3f;
    private float canFire = -1;

    public bool IsDying { get; private set; }

    Player player;
    //Animator animator;

    private void Start()
    {
        enemySpeed = gameplayConfig.EnemyMovementSpeed;

        player = Player.Instance;

        /*animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Enemy: Animator reference is null.");
        }*/

        SetFireRate();
    }

    void Update()
    {
        if (IsDying)
        {
            return;
        }

        EnemyMovement();

        if (Time.time > canFire)
        {
            SetFireRate();

            // Gets the laser offset and applying it relative to the enemy's rotation
            Vector3 laserSpawnPosition = transform.TransformPoint(laserOffset);
            Instantiate(enemyLaserPrefab, laserSpawnPosition, transform.rotation);
        }
    }

    // Moves enemy downwards while player is alive, and respawns at a random position at the top of the screen if it goes off the bottom
    private void EnemyMovement()
    {
        float randomPositionX = Random.Range(-9.4f, 9.4f);

        transform.position += transform.up * Time.deltaTime * enemySpeed;

        if (transform.position.y < -6.1f && player != null)
        {
            transform.position = new Vector3(randomPositionX, 7.5f, transform.position.z);
        }
    }

    private void SetFireRate()
    {
        fireRate = Random.Range(minFireRate, maxFireRate);
        canFire = Time.time + fireRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDying)
        {
            return;
        }
        CheckPlayerCollision(other);
    }

    // Checks whether enemy colliding with player, and if so causes player damage then destroys enemy
    private void CheckPlayerCollision(Collider other)
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
        HandleImpact(false);
    }

    public void HandleImpact(bool giveScore)
    {
        if (giveScore)
        {
          player.AddScore();  
        }
        IsDying = true;
        enemySpeed = 0f;
        //animator.SetTrigger("OnEnemyDeath");
        AudioManager.Instance.PlayAudio(AudioType.Explosion);
        Destroy(this.gameObject);
    }
}