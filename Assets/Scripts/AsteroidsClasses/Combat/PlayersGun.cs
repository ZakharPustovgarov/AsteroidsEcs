using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Данный класс отвечает за произведение выстрелов игроком
public class PlayersGun : MonoBehaviour
{
    // Точка, откуда производится выстрел
    [SerializeField]
    Transform firePoint;
    // Точка, в сторону которой производится выстрел
    [SerializeField]
    Transform fireDirection;
    // Позиция корабля
    [SerializeField]
    Transform ship;

    // Префабы лазера и пули в разных типах отображения
    [SerializeField]
    GameObject laserPolyPrefab, laserSpritePrefab;
    [SerializeField]
    GameObject bulletPolyPrefab, bulletSpritePrefab;

    // Скорость пули
    public float bulletSpeed = 8f;
    // Интервал между выстрелами пулями
    public float bulletCooldown = 0.2f;
    // Таймер для отсчитываня интервала
    float timer = 0f;

    // Время до исчезновения лазера
    public float laserExparationTime = 4f;
    // Переменная для отслеживания, есть ли актиынй лазер
    bool isShootingLaser = false;

    PlayerManager playerManager;

    GameManager gameManager;

    [SerializeField]
    public Sprite otherSprite;

    void Start()
    {
        playerManager = PlayerManager.Instance;

        gameManager = GameManager.Instance;

        isShootingLaser = false;
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    // Функция для произведения выстрелов лазером или пулями
    public void Fire(int fireMode)
    {
        if (isShootingLaser) return;

        if(fireMode == 1 && timer <= 0) // Если режим ведения огня равен единице, таймер интервала меньше или равен нулю и нет активного лазера, то...
        {
            Bullet bullet;// Создаём объект пули

            // В зависимости от текущего типа визуализации присваиваем объекту соответсвующий префаб
            if(gameManager.Visualization == false) bullet = GameObject.Instantiate(bulletPolyPrefab, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Bullet>();
            else bullet = GameObject.Instantiate(bulletSpritePrefab, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Bullet>();

            Vector2 vec = fireDirection.position - firePoint.position;// Ищем направление выстрела

            bullet.GetComponent<Rigidbody2D>().AddForce(vec * bulletSpeed);// Задаём силу, отправляющую пулю в полёт

            timer = bulletCooldown;// Ставим таймер на интервал выстрелов
        }
        else if(fireMode == 2 && playerManager.laserCount > 0) // Если режим ведения огня равен двойке,  нет активного лазера и есть выстрелы лазером в запасе, то..
        {
            StartCoroutine("CreateLaser");// Создаём лазер
        }
    }

    // Корутина, отвечающая за жизненный цикл лазера
    IEnumerator CreateLaser()
    {
        isShootingLaser = true;

        playerManager.LaserUsed();

        GameObject laser;

        if (gameManager.Visualization == false) laser = GameObject.Instantiate(laserPolyPrefab, firePoint);
        else laser = GameObject.Instantiate(laserSpritePrefab, firePoint);

        yield return new WaitForSeconds(laserExparationTime);

        GameObject.Destroy(laser);

        isShootingLaser = false;
    }
}