using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField]
    GameObject visibleShield;

    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;

    [SerializeField]
    private float movementSpeed = 5;
    private float speedBoostMultiplier = 2;
    [SerializeField]
    private int playerLives = 3;
    [SerializeField]
    private int score;
    [SerializeField]
    private float fireRate = 0.2f;
    private float canFire = -1f;
    private bool isTripleShotActive = false;
    private bool isSpeedBoostActive = false;
    private bool isShieldActive = false;

    // Event
    public Action OnDeath;
    public Action<int> OnScoreUpdated;
    public Action<int> OnLivesUpdated;

    void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        // Set start position
        transform.position = new Vector3(0, 0, 0);
        OnLivesUpdated?.Invoke(playerLives);
        OnScoreUpdated?.Invoke(score);

        visibleShield.SetActive(false);
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
        Vector3 screenBoundsBottom = new Vector3(transform.position.x, -3.51f, transform.position.z);

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
        canFire = Time.time + fireRate;

        if (isTripleShotActive)
        {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Vector3 laserSpawnPosition = new Vector3(transform.position.x, transform.position.y + 1.16f, transform.position.z);
            Instantiate(laserPrefab, laserSpawnPosition, Quaternion.identity);
        }
    }

    // Player loses lives based on amount of damage dealt (called by enemy), and destroys player if lives reach 0
    public void TakeDamage(int damageDealt)
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            visibleShield.SetActive(false);
            return;
        }
        
        playerLives -= damageDealt;
        OnLivesUpdated?.Invoke(playerLives);

        if (playerLives < 1)
        {
            OnDeath?.Invoke();
            Destroy(this.gameObject);
        }
    }

    public void AddScore()
    {
        score += 10;
        OnScoreUpdated?.Invoke(score);
    }

    public void ActivateTripleShot()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        isTripleShotActive = false;
    }

    public void ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        movementSpeed *= speedBoostMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        movementSpeed /= speedBoostMultiplier;
        isSpeedBoostActive = false;
    }

    public void ActivateShield()
    {
        isShieldActive = true;
        visibleShield.SetActive(true);
        //StartCoroutine(ShieldPowerDownRoutine());
    }

  /*  IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Shield expired");
        //spriteRenderer.enabled = false;
        isShieldActive = false;
    }*/
}