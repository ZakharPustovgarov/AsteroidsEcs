using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс пули
public class OldBullet : DoDamage
{
    // Время до самоуничтожения
    [SerializeField]
    float selfDestroyTime = 8f;

    // При включении/поялвении ставится тип урона и запускается отсчёт до самоуничтожения
    void OnEnable()
    {
        damageType = "Bullet";

        StartCoroutine("SelfDestroy");
    }

    // Функция нанесения урона
    protected override void Damage(Collider2D enemy)
    {
        base.Damage(enemy);

        GameObject.Destroy(this.gameObject);
    }

    // Корутина для самоуничтожения
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(selfDestroyTime);

        GameObject.Destroy(this.gameObject);
    }
}
