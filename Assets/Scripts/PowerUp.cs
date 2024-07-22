using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float powerupSpeed = 3f;
    [SerializeField]
    private int powerupID;

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

        // TODO check which powerup

        player.ActivateTripleShot();
        Destroy(this.gameObject);
        
    }
}
