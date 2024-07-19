using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    float laserSpeed = 10.0f;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * laserSpeed);

        if (transform.position.y > 8f)
        {
            Destroy(this.gameObject);
        }
    }
}
