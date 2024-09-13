using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float laserOffset;
    [SerializeField] private GameObject visibleShield;
    [SerializeField] private GameObject[] damagedEngines;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleShotPrefab;
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float speedBoostMultiplier = 2;
    [SerializeField] private int playerLives = 3;
    [SerializeField] private float fireRate = 0.5f;

    private Animator animator;

    private float canFire = -1f;
    private bool isTripleShotActive = false;
    private bool isSpeedBoostActive = false;

    public bool IsInvincible = false;

    //[SerializeField] private int score;
    //public int Score => score;
    // Access this in any other classes, but only set in this class. Alternate to having private score var above.
    public int Score { get; private set; }
    public bool IsShieldActive { get; private set; }


    // Events
    public Action OnDeath;
    public Action<int> OnScoreUpdated;
    public Action<int> OnLivesUpdated;

    public static Player Instance;

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
        OnScoreUpdated?.Invoke(Score);

        visibleShield.SetActive(false);
        damagedEngines[0].SetActive(false);
        damagedEngines[1].SetActive(false);

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Player: Animator reference is null.");
        }
    }

    void Update()
    {
        PlayerMovement();
        PlayerBounds();

        if (Input.GetKey(KeyCode.Space) && Time.time > canFire)
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
            animator.SetBool("OnMoveLeft", true);
        }
        else
        {
            animator.SetBool("OnMoveLeft", false);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
            animator.SetBool("OnMoveRight", true);
        }
        else
        {
            animator.SetBool("OnMoveRight", false);
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
            Vector3 laserSpawnPosition = new Vector3(transform.position.x, transform.position.y + laserOffset, transform.position.z);
            Instantiate(laserPrefab, laserSpawnPosition, Quaternion.identity);
        }

        AudioManager.Instance.PlayAudio(AudioType.FireLaser);
    }

    // Player loses lives based on amount of damage dealt (called by enemy), and destroys player if lives reach 0
    public void TakeDamage(int damageDealt)
    {
        if (IsShieldActive)
        {
            IsShieldActive = false;
            visibleShield.SetActive(false);
            return;
        }

        if (IsInvincible)
        {
            return;
        }

        playerLives -= damageDealt;
        OnLivesUpdated?.Invoke(playerLives);

        // TODO: Randomise Engine
        switch(playerLives)
        {
            case 2:
                damagedEngines[0].SetActive(true);
                break;
            case 1:
                damagedEngines[1].SetActive(true);
                break;
            default:
                break;

        }
        if (playerLives < 1)
        {
            OnDeath?.Invoke();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void AddScore()
    {
        //TODO add to gameManager
        Score += 10;
        OnScoreUpdated?.Invoke(Score);
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
        if (isSpeedBoostActive)
        {
            return;
        }
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
        IsShieldActive = true;
        visibleShield.SetActive(true);
    }
}