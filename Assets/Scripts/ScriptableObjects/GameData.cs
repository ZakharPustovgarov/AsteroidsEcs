using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Data", menuName = "ScriptableObjects/GameData", order = 52)]
public class GameData : ScriptableObject
{
    public KeyCode FlyForwardKey = KeyCode.W;
    public KeyCode FireBulletKey = KeyCode.Keypad1;
    public KeyCode FireLaserKey = KeyCode.Keypad2;
    public KeyCode RotateLeftKey = KeyCode.Q;
    public KeyCode RotateRightKey = KeyCode.E;

    public float FlyingSpeed = 10f;
    public float RotateSpeed = 5f;

    public float BulletFlyingSpeed = 15f;

    public float UfoSpawnTime = 7f;
    public float UfoFlyingSpeed = 6f;

    public float AsteroidSpawnTime = 5f;
    public float BigAsteroidSpeed = 2f;
    public int MaxSmallAsteroids = 3;
    public float SmallAsteroidSpeed = 4f;

    public int ScoreForUFO = 15;
    public int ScoreForBigAsteroid = 10;
    public int ScoreForSmall = 5;

    public Bullet BulletPrefab;

    public Laser LaserPrefab;

    public UfoEcs UfoPrefab;

    public AsteroidEcs BigAsteroidPrefab;

    public AsteroidEcs SmallAsteroidPrefab; 
}
