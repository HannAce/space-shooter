using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Player player = Player.Instance;

    [SerializeField]
    private float powerUpSpeed = 3f;

    void Update()
    {
        PowerUpMovement(); 
    }

    private void PowerUpMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * powerUpSpeed);

        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
