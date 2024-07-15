using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Set start position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerBounds();
    }

    // Input for player movement
    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }

    // Check if player goes out of bounds, and wrap them to the other side of the screen
    void PlayerBounds()
    {
        Vector3 screenBoundsRight = new Vector3(11.2f, transform.position.y, transform.position.z);
        Vector3 screenBoundsLeft = new Vector3(-11.2f, transform.position.y, transform.position.z);
        Vector3 screenBoundsTop = new Vector3(transform.position.x, 7.3f, transform.position.z);
        Vector3 screenBoundsBottom = new Vector3(transform.position.x, -5.3f, transform.position.z);


        if (transform.position.x >= screenBoundsRight.x)
        {
            transform.position = screenBoundsLeft;
        }
        else if (transform.position.x <= screenBoundsLeft.x)
        {
            transform.position = screenBoundsRight;
        }
        if (transform.position.y >= screenBoundsTop.y)
        {
            transform.position = screenBoundsBottom;
        }
        else if (transform.position.y <= screenBoundsBottom.y)
        {
            transform.position = screenBoundsTop;
        }
    }
}