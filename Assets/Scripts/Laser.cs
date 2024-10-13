using System;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] protected GameplayConfig gameplayConfig;

    [SerializeField] private string damagingTag;
    [SerializeField] private GameObject enemyLaserExplosionPrefab;
    [SerializeField] private float playerLaserSpeed = 15.0f;

    protected virtual void Start()
    {
        playerLaserSpeed = gameplayConfig.PlayerLaserSpeed;
    }

    void Update()
    {
        MoveLaser();
        CheckBoundaries();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != damagingTag)
        {
            return;
        }

        switch (damagingTag)
        {
            case "Player":
                HandlePlayerHit();
                break;
            case "Enemy":
                HandleEnemyHit(other);
                break;
        }
    }

    private void HandleEnemyHit(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("Enemy is null.");
            return;
        }

        if (enemy.IsDying)
        {
            return;
        }

        enemy.HandleImpact(true);
        DestroyLaser();
    }
    private void HandlePlayerHit()
    {
         if (Player.Instance.IsShieldActive)
        {
            ReflectLaser();
            //Player.Instance.TakeDamage(1); Add in if want lasers to destroy shield
            return;
        }

        Player.Instance.TakeDamage(1);
        AudioManager.Instance.PlayAudio(AudioType.Explosion);
        Instantiate(enemyLaserExplosionPrefab, transform.position, Quaternion.identity);
        DestroyLaser();
    }

    protected virtual void MoveLaser()
    {
        transform.position += transform.up * Time.deltaTime * playerLaserSpeed;
    }

    private void CheckBoundaries()
    {
        if (transform.position.y > 8f || transform.position.y < -8f)
        {
            DestroyLaser();
        }
    }

    
    private void ReflectLaser()
    {
        transform.Rotate(new Vector3(0, 0, 180));
        if (damagingTag == "Player")
        {
            damagingTag = "Enemy";
            //// TODO - Figure out direction based reflecting
            
            //// Get a vector direction relative from the laser to the player
            //// Normalized means every value is from 0-1
            //Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
            //// Turn the direction into a rotation that can be applied
            //Quaternion targetRotation = Quaternion.LookRotation(direction);
            //transform.rotation = targetRotation;
            //transform.Rotate(new Vector3(0, 90, 0));
        }
        else
        {
            damagingTag = "Player";
        }
    }

    protected void DestroyLaser()
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(this.gameObject);
    }
}
