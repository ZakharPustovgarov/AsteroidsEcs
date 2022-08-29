using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс осколка астероида
public class SmallAsteroid : Enemy
{
    // Rigidbody астероида
    Rigidbody2D rigidbody;

    // При активации вызывается метод движения
    void OnEnable()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();

        damageType = "Asteroid";

        destination = FindDestination();

        StartCoroutine("MoveDelay");
    }

    // На астероид подаётся сила, отправляющая его дрейфовать по направлению движения
    protected override void Move()
    {
        rigidbody.AddForce((destination.position - this.transform.position) * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Функция для выбора направления
    protected Transform FindDestination()
    {
        return GameObject.Find("DestinationPoint (" + (int)UnityEngine.Random.Range(0,40) + ")").transform;
    }

    // Корутина для небольшой задержки перед началом движения. Нужна для того, чтобы после создания осколков большим астероидом при уничтожении было время присвоить необходимые значения осколку
    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(0.5f);

        Move();
    }

    // Функция, которая вызывается большим астероидом после назначения необходимых значений, которая откулючает корутину и начинает движение
    public void ChangeDestination(Transform newDestination)
    {
        destination = newDestination;

        StopCoroutine("MoveDelay");

        Move();
    }
}
