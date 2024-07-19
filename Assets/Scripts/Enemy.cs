using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * enemySpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}