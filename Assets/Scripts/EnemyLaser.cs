using System;
using UnityEngine;

public class EnemyLaser : Laser
{
    private float enemyLaserSpeed = 5.0f;

    protected override void MoveLaser()
    {
        transform.position += transform.up * Time.deltaTime * enemyLaserSpeed;
    }
}
