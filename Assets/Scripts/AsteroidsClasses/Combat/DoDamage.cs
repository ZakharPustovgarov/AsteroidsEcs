using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Родительский класс для любых объектов, которые наносят урон
public class DoDamage : MonoBehaviour
{
    // Тип урона
    [SerializeField]
    protected string damageType = "";

    // Тэги объектов, которым будет нанесён урон
    public List<string> tagsToDamage;

    // Спрайт для другого типа отображения
    [SerializeField]
    public Sprite otherSprite;

    SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Проверка тэга соприкоснувшегося объекта
    void OnTriggerEnter2D(Collider2D other)
    {
        if (TagCheck(other.tag))
        {
            Damage(other);
        }
    }

    // Нанесение урона соответсвующим типом
    protected virtual void Damage(Collider2D enemy)
    {
        enemy.GetComponent<IDamagable>().TakeDamage(damageType);
    }

    // Функция на проверку наличия тэга в tagsToDamage
    protected bool TagCheck(string otherTag)
    {
        foreach(string tag in tagsToDamage)
        {
            if (tag == otherTag) return true;
        }

        return false;
    }

    // Функция для смены спрайта
    public virtual void ReplaceSprite()
    {
        Sprite buf = spriteRenderer.sprite;

        spriteRenderer.sprite = otherSprite;

        otherSprite = buf;
    }
}
