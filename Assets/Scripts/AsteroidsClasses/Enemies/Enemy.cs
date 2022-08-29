using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Родительский класс для классов врагов
public abstract class Enemy : DoDamage, IDamagable
{
    // Направление движения
    [SerializeField]
    protected Transform destination;

    // Счёт, который начислаяется за уничтожение врага
    public int score = 10;

    // Скорость движения
    public float speed = 0.8f;

    // Добавляется тэг игрока в список тэгов, по которым наносится урон
    protected virtual void Start()
    {
        tagsToDamage.Add("Player");
    }

    // Метод движения
    protected abstract void Move();

    // Метод получения урона
    public virtual void TakeDamage(string damageType)
    {
        GameManager.Instance.AddScore(score);

        GameObject.Destroy(this.gameObject);
    }
}
