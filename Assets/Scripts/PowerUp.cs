using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Player player = Player.Instance;

    [SerializeField]
    private float powerupSpeed = 3f;
    [SerializeField]
    private int powerupID; // 0 = Triple Shot, 1 = Speed, 2 = Shield     TODO: Update to enum later???

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
        CheckPlayerCollision(other);
    }

    // Check if collision is with player
    private void CheckPlayerCollision(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        if (player == null)
        {
            Debug.LogError("PowerUp Script: Player reference is null.");
            return;
        }

        CheckPowerup();
    }

    // Check ID of the powerup collided with, and activate that powerup
    private void CheckPowerup()
    {

        if (powerupID == 0)
        {
            player.ActivateTripleShot();
            Destroy(this.gameObject);
        }
        else if (powerupID == 1)
        {
            player.ActivateSpeedBoost();
            Destroy(this.gameObject);
        }
        else if(powerupID == 2)
        {
            player.ActivateShield();
            Destroy(this.gameObject);
        }

        /*switch (powerupID)
        {
            case 0:
                player.ActivateTripleShot();
                Destroy(this.gameObject);
                break;

            case 1:
                player.ActivateSpeedBoost();
                Destroy(this.gameObject);
                break;

            case 2:
                player.ActivateShield();
                Destroy(this.gameObject);
                break;

            default:
                Debug.LogWarning("Unknown Powerup ID");
                break;
        }*/
    }
}
