using System;
using UnityEngine;

public class EnemyLaser : Laser
{
    private float enemyLaserSpeed = 5.0f;

    protected override void Start()
    {
        //base.Start(); // runs the higher class start for Laser.Start as well as this one
        enemyLaserSpeed = gameplayConfig.EnemyLaserSpeed;
    }

    protected override void MoveLaser()
    {
        transform.position += transform.up * Time.deltaTime * enemyLaserSpeed;
    }
}
