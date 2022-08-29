using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������������ ����� ��� ������� ������
public abstract class Enemy : DoDamage, IDamagable
{
    // ����������� ��������
    [SerializeField]
    protected Transform destination;

    // ����, ������� ������������ �� ����������� �����
    public int score = 10;

    // �������� ��������
    public float speed = 0.8f;

    // ����������� ��� ������ � ������ �����, �� ������� ��������� ����
    protected virtual void Start()
    {
        tagsToDamage.Add("Player");
    }

    // ����� ��������
    protected abstract void Move();

    // ����� ��������� �����
    public virtual void TakeDamage(string damageType)
    {
        GameManager.Instance.AddScore(score);

        GameObject.Destroy(this.gameObject);
    }
}
