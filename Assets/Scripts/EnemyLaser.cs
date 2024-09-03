using UnityEngine;

public class EnemyLaser : Laser
{
    [SerializeField] private GameObject enemyLaserExplosionPrefab;
    private float enemyLaserSpeed = 5.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        Player.Instance.TakeDamage(1);
        AudioManager.Instance.PlayAudio(AudioType.Explosion);
        Instantiate(enemyLaserExplosionPrefab, transform.position, Quaternion.identity);
        DestroyLaser();
    }

    protected override void MoveLaser()
    {
        transform.Translate(Vector3.down * Time.deltaTime * enemyLaserSpeed);

        if (transform.position.y < -7f)
        {
            DestroyLaser();
        }
    }
}
