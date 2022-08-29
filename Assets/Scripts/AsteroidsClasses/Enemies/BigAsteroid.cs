using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� �������� ���������
public class BigAsteroid : SmallAsteroid
{
    // ������ �����������, � ������� ����� �������� ������� ����� ����������� ��������� ������
    [SerializeField]
    Transform[] directions;

    // ������ ������� ���������
    [SerializeField]
    GameObject smallAsteroidPolyPrefab;
    [SerializeField]
    GameObject smallAsteroidSpritePrefab;

    // ������� ��������� �����
    public override void TakeDamage(string damageType)
    {
        if(damageType != "Laser")// ���� ���� ������� �� �� ������, ��
        {
            int count = (int)UnityEngine.Random.Range(1, 3);// ������ ��������� ����� �� 1 �� 3, ������� �������� ����������� ���������, �� ������� ���������� �������� ��� ����������� ������

            List<int> occupiedDirections = new List<int>();// �������� ������, � ������� ����� �������� ������������ �����������

            for (int i = 0; i < count; i++)
            {
                SmallAsteroid asteroid;

                if(GameManager.Instance.Visualization == false) asteroid = GameObject.Instantiate(smallAsteroidPolyPrefab, this.transform.position, this.transform.rotation).GetComponent<SmallAsteroid>();// �������� ������ �������
                else asteroid = GameObject.Instantiate(smallAsteroidSpritePrefab, this.transform.position, this.transform.rotation).GetComponent<SmallAsteroid>();

                int directionNumber = FindFreeDirection(occupiedDirections);// ������ ��������� �����������

                asteroid.speed *= 4f;// �������� ������� ���������� �� 4

                occupiedDirections.Add(directionNumber);// � ������ ������� ���������� ����������� ��������� �����������

                asteroid.ChangeDestination(directions[directionNumber]);// ����� ����������� � �������
            }
        }

        base.TakeDamage(damageType); // ���� ������ ������������
    }

    // ������� ��� ���������� ���������� �����������
    int FindFreeDirection(List<int> occupiedDirections)
    {
        int directionNumber = (int)UnityEngine.Random.Range(0, 11);

        if(occupiedDirections.Count == 0)// ���� � ������ ������� ����������� ��� ���������, �� ������ ��������� ���������� �����
        {
            return directionNumber;
        }
        else// ����� ���� ��������� ����� �� ��� ���, ���� ��� �� ����� � ������, ����� ���� ���������� ���
        {
            while(occupiedDirections.Contains(directionNumber))
            {
                directionNumber = (int)UnityEngine.Random.Range(0, 11);
            }

            return directionNumber;
        }
    }
}
