using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������������ ����� ��� ����� ��������, ������� ������� ����
public class DoDamage : MonoBehaviour
{
    // ��� �����
    [SerializeField]
    protected string damageType = "";

    // ���� ��������, ������� ����� ������ ����
    public List<string> tagsToDamage;

    // ������ ��� ������� ���� �����������
    [SerializeField]
    public Sprite otherSprite;

    SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // �������� ���� ����������������� �������
    void OnTriggerEnter2D(Collider2D other)
    {
        if (TagCheck(other.tag))
        {
            Damage(other);
        }
    }

    // ��������� ����� �������������� �����
    protected virtual void Damage(Collider2D enemy)
    {
        enemy.GetComponent<IDamagable>().TakeDamage(damageType);
    }

    // ������� �� �������� ������� ���� � tagsToDamage
    protected bool TagCheck(string otherTag)
    {
        foreach(string tag in tagsToDamage)
        {
            if (tag == otherTag) return true;
        }

        return false;
    }

    // ������� ��� ����� �������
    public virtual void ReplaceSprite()
    {
        Sprite buf = spriteRenderer.sprite;

        spriteRenderer.sprite = otherSprite;

        otherSprite = buf;
    }
}
