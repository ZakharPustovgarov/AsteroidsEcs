using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ����� �������� ������, ���������� �� ���� � ���������� �������, ��������� �� ���������� �� �����, � ����� �� ���� ������ ������
public class PlayerManager : MonoSingleton<PlayerManager>
{
    // �������� ��������� ������ ������
    [SerializeField]
    Animation animDeathScreen;

    // �����, � ������� ������� ���������� ���������� �������
    [SerializeField]
    Text laserCountText;

    // �����, � ������� ������� ���� �� ������ ������
    [SerializeField]
    Text deathScoreValue;

    PlayerController player;

    // ��������, ������� ��������� ��������
    public PlayerController Player
    { get { return player; } }

    // ��� �� �����?
    bool isPlayerAlive;

    // ���������� ��� �������� � ������������� ���������� �������
    public int laserCount, maxLaserCount = 4;

    // ����� ����������� ������
    [SerializeField]
    float laserRechargeTime = 17f;

    // ���������� �� � ������ ������ �����������?
    bool isRecharging;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        isRecharging = false;

        isPlayerAlive = true;   

        laserCount = maxLaserCount;

        player.OnDeath += this.OnDeath;
    }

    // �������, ������� ����������� �������� ��������� ������ ������, � ����� ��� ������ �������� ����, ��� ����� ����
    void OnDeath()
    {
        if (isPlayerAlive == true)
        {
            isPlayerAlive = false;

            GameManager.Instance.isPlayerAlive = false;

            deathScoreValue.text = Convert.ToString(GameManager.Instance.Score);

            animDeathScreen.Play();
        }    
    }
    
    // ���������� ���������� ���������� ������� � ���������� ������
    void UpdateLaserCount()
    {
        laserCountText.text = Convert.ToString(laserCount);
    }

    // �������, ������� ���������� ��� ������������� ������, ��� ���������� ���������� ��������� ������� � ������ �����������
    public void LaserUsed()
    {
        laserCount--;

        UpdateLaserCount();

        if (isRecharging == false)
        {
            StartCoroutine("LaserRecharge");
        }       
    }

    // �������� ���������� �������
    IEnumerator LaserRecharge()
    {
        isRecharging = true;

        while(laserCount < maxLaserCount)
        {
            yield return new WaitForSeconds(laserRechargeTime);

            laserCount++;

            UpdateLaserCount();
        }

        isRecharging = false;
    }
}
