using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    private float laserSpeed = 12.0f;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * laserSpeed);

        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
