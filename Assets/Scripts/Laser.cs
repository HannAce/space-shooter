using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private GameObject enemyLaserExplosionPrefab;

    private float playerLaserSpeed = 12.0f;
    private float enemyLaserSpeed = 5.0f;

    void Update()
    {
        if (tag == "Weapon")
        {
            MoveUp();
        }
        else if (tag == "Enemy")
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * Time.deltaTime * playerLaserSpeed);

        if (transform.position.y > 8f)
        {
            DestroyLaser();
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * enemyLaserSpeed);

        if (transform.position.y < -7f)
        {
            DestroyLaser();
        }
    }

    private void DestroyLaser()
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        if (tag == "Enemy")
        {
            Player.Instance.TakeDamage(1);
            AudioManager.Instance.PlayAudio(AudioType.Explosion);
            Instantiate(enemyLaserExplosionPrefab, transform.position, Quaternion.identity);
            DestroyLaser();
        }
    }
}
