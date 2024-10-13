using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerupType
    {
        None = -1,
        TripleShot = 0,
        Speed = 1,
        Shield = 2
    }

    [SerializeField] private float powerupSpeed = 3f;
    [SerializeField] private PowerupType powerupID;

    Player player;

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

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckPlayerCollision(other))
        {
            return;
        }

        CheckPowerup();
    }

    // Check if collision is with player
    private bool CheckPlayerCollision(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return false;
        }

        if (player == null)
        {
            Debug.LogError("PowerUp.CheckPlayerCollision(): Player reference is null.");
            return false;
        }
       
        return true;
    }

    // Check ID of the powerup collided with, and activate that powerup
    private void CheckPowerup()
    {
        switch (powerupID)
        {
            case PowerupType.TripleShot:
                player.ActivateTripleShot();
                break;

            case PowerupType.Speed:
                player.ActivateSpeedBoost();
                break;

            case PowerupType.Shield:
                player.ActivateShield();
                break;

            default:
                Debug.LogWarning("Unknown Powerup");
                break;
        }

        AudioManager.Instance.PlayAudio(AudioType.CollectPowerup);
        Destroy(this.gameObject);
    }
}
