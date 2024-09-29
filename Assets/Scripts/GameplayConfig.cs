using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Hannah/GameplayConfig")]
public class GameplayConfig : ScriptableObject
{
    [Header("Player")]
    public float PlayerMovementSpeed;
    public int PlayerStartingLives;
    public float PlayerFireRate;
    public float PlayerLaserSpeed;

    [Header("Enemy")]
    public float EnemyMovementSpeed;
    public float EnemyLaserSpeed;
}
