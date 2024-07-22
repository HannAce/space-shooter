using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
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
        Player player = Player.Instance;

        if (other.gameObject.tag != "Player")
        {
            return;
        }

        if (player == null)
        {
            Debug.LogError("PowerUp Script: Player reference is null.");
            return;
        }

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

    // Check if collision is with player

    // Check ID of the powerup collided with, and activate that powerup
}
