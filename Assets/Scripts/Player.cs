using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 5f;

    public GameObject laserPrefab;

    void Start()
    {
        // Set start position
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        PlayerMovement();
        PlayerBounds();
        FireLaser();
    }

    // Input for player movement
    void PlayerMovement()
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

        /*
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * Time.deltaTime * speed);
        */
    }

    // Check if player goes out of bounds, and wrap them to the other side of the screen
    void PlayerBounds()
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

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }
}