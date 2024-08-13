using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private SpawnManager spawnManager;

    [SerializeField] private float rotateSpeed = 30;

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            Destroy(other.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            spawnManager.StartSpawning();
            AudioManager.Instance.PlayAudio(AudioType.Explosion);
            Destroy(this.gameObject, 0.25f);
        }
    }
}
