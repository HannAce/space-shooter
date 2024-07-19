using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private int playerLives = 3;
    [SerializeField]
    private float fireRate = 0.2f;
    private float canFire = -1f;

    void Start()
    {
        // Set start position
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        PlayerMovement();
        PlayerBounds();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            FireLaser();
        }
    }

    // Input for player movement
    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * movementSpeed);
        }
    }

    // Check if player goes out of bounds, and wrap them to the other side of the screen
    private void PlayerBounds()
    {
        Vector3 screenBoundsRight = new Vector3(11.28f, transform.position.y, transform.position.z);
        Vector3 screenBoundsLeft = new Vector3(-11.28f, transform.position.y, transform.position.z);
        Vector3 screenBoundsTop = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 screenBoundsBottom = new Vector3(transform.position.x, -3.9f, transform.position.z);

        // TODO refactor later
        if (transform.position.y >= screenBoundsTop.y)
        {
            transform.position = screenBoundsTop;
        }
        else if (transform.position.y <= screenBoundsBottom.y)
        {
            transform.position = screenBoundsBottom;
        }
        if (transform.position.x >= screenBoundsRight.x)
        {
            transform.position = screenBoundsLeft;
        }
        else if (transform.position.x <= screenBoundsLeft.x)
        {
            transform.position = screenBoundsRight;
        }
    }

    // Input to fire laser, and cooldown between firing
    private void FireLaser()
    {
        Vector3 laserSpawnPosition = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);

        canFire = Time.time + fireRate;
        Instantiate(laserPrefab, laserSpawnPosition, Quaternion.identity);
    }

    public void TakeDamage(int damageDealt)
    {
        playerLives -= damageDealt;
        Debug.Log("Lives remaining: " + playerLives);

        if (playerLives <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}