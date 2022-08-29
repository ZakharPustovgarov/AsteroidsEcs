using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Класс менджера игрока, отвечающий за учёт и перезардку лазеров, выведения их количества на экран, а также за взов экрана смерти
public class PlayerManager : MonoSingleton<PlayerManager>
{
    // Анимация появления экрана смерти
    [SerializeField]
    Animation animDeathScreen;

    // Текст, в котором пишется количество оставшихся лазеров
    [SerializeField]
    Text laserCountText;

    // Текст, в котором пишется счёт на экране смерти
    [SerializeField]
    Text deathScoreValue;

    PlayerController player;

    // Свойство, которое позволяет получить
    public PlayerController Player
    { get { return player; } }

    // Жив ли игрок?
    bool isPlayerAlive;

    // Переменные для текущего и максимального количества лазеров
    public int laserCount, maxLaserCount = 4;

    // Время перезарядки лазера
    [SerializeField]
    float laserRechargeTime = 17f;

    // Происходит ли в данный момент перезарядка?
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

    // Функция, которая проигрывает анимация появления экрана смерти, а также даёт понять менджеру игры, что игорк умер
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
    
    // Обновления количества оставшихся лазеров в интерфейсе игрока
    void UpdateLaserCount()
    {
        laserCountText.text = Convert.ToString(laserCount);
    }

    // Функция, которая вызывается при использвоании лазера, для уменьшения количества доступных лазеров и начало перезарядки
    public void LaserUsed()
    {
        laserCount--;

        UpdateLaserCount();

        if (isRecharging == false)
        {
            StartCoroutine("LaserRecharge");
        }       
    }

    // Корутина перезардки лазеров
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
