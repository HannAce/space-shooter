using UnityEngine;

public class Laser : MonoBehaviour
{
    private float playerLaserSpeed = 12.0f;


    void Update()
    {
        MoveLaser();
    }

    protected virtual void MoveLaser()
    {
        transform.Translate(Vector3.up * Time.deltaTime * playerLaserSpeed);

        if (transform.position.y > 8f)
        {
            DestroyLaser();
        }
    }

    protected void DestroyLaser()
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(this.gameObject);
    }
}
