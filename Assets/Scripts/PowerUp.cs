using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Player player;

    [SerializeField]
    private float powerupSpeed = 3f;
    [SerializeField]
    private int powerupID; // 0 = Triple Shot, 1 = Speed, 2 = Shield     TODO: Update to enum later???

    private void Start()
    {
        player = Player.Instance;
    }
    void Update()
    {
        PowerupMovement();
    }

    private void PowerupMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * powerupSpeed);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!CheckPlayerCollision(other))
        {
            return;
        }

        CheckPowerup();
    }

    // Check if collision is with player
    private bool CheckPlayerCollision(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            return false;
        }

        if (player == null)
        {
            Debug.LogError("PowerUp Script: Player reference is null.");
            return false;
        }
       
        return true;
    }

    // Check ID of the powerup collided with, and activate that powerup
    private void CheckPowerup()
    {
        switch (powerupID)
        {
            case 0:
                player.ActivateTripleShot();
                break;

            case 1:
                player.ActivateSpeedBoost();
                break;

            case 2:
                player.ActivateShield();
                break;

            default:
                Debug.LogWarning("Unknown Powerup ID");
                break;
        }

        Destroy(this.gameObject);
    }
}
