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

    public GameObject PolyBulletPrefab;
    public GameObject SpriteBulletPrefab;

    public GameObject PolyLaserPrefab;
    public GameObject SpriteLaserPrefab;

    public GameObject PolyUfoPrefab;
    public GameObject SpriteUfoPrefab;

    public GameObject PolyBigAsteroidPrefab;
    public GameObject SpriteBigAsteroidPrefab;
    public GameObject PolySmallAsteroidPrefab; 
    public GameObject SpriteSmallAsteroidPrefab;
}
