using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Класс менеджера игры, отвечающий за создание новых противников, ведение счёта и смену спрайтов
public class GameManager : MonoSingleton<GameManager>
{
    // Точки, в которых может появиться новый враг
    [SerializeField]
    Transform[] spawnPoints;

    // Счёт
    int score;
    public int Score
    { get { return score; } }

    // Текст, отображющий счёт в интерфейсе игрока
    [SerializeField]
    Text scoreText;

    // Жив ли игрок?
    public bool isPlayerAlive;

    // Префабы врагов в разных типах отображения
    [SerializeField]
    GameObject bigAsteroidPolyPrefab, ufoPolyPrefab, bigAsteroidSpritePrefab, ufoSpritePrefab;

    // Время, через которое появляется определённый вид врагов
    [SerializeField]
    float asteroidSpawnTimer, ufoSpawnTimer;

    // Переменная, нужная для того, чтобы враги не появлялись на одном и том же месте друг за другом
    int previousSpawnNumber = -1;

    // Текущий тип отображения
    bool visualization;

    // Свойство для получения текущего типа отображения
    public bool Visualization
    { get { return visualization; } }

    //
    PlayerController player = null;

    // При выходе из зоны игры объекты. которые не являются лазером, уничтожаются
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag != "Laser")
        {
            GameObject.Destroy(other.gameObject);
        }
    }

    // В начале игры счёт ставится на 0 и запускаются корутины поялвения врагов
    void Start()
    {
        score = 0;

        isPlayerAlive = true;

        player = PlayerManager.Instance.Player;

        if (player == null)
        {
            StartCoroutine("AcquirePlayer");
        }
        else player.OnSpriteChange += ChangeVisualization;

        StartCoroutine("UfoSpawn");
        StartCoroutine("AsteroidSpawn");
    }

    IEnumerator AcquirePlayer()
    {
        while(player == null)
        {
            player = PlayerManager.Instance.Player;
            yield return new WaitForSeconds(0.05f);
        }

        player.OnSpriteChange += ChangeVisualization;
    }

    //Корутина повяления НЛО
    IEnumerator AsteroidSpawn()
    {
        while (isPlayerAlive == true)
        {
            Transform spawn = FindSpawnPoint();

            if (visualization == false) GameObject.Instantiate(bigAsteroidPolyPrefab, spawn.position, spawn.rotation);          
            else GameObject.Instantiate(bigAsteroidSpritePrefab, spawn.position, spawn.rotation);


            yield return new WaitForSeconds(asteroidSpawnTimer);
        }      
    }

    //Корутина повяления астероидов
    IEnumerator UfoSpawn()
    {
        while (isPlayerAlive == true)
        {
            Transform spawn = FindSpawnPoint();

            if (visualization == false)  GameObject.Instantiate(ufoPolyPrefab, spawn.position, spawn.rotation);
            else GameObject.Instantiate(ufoSpritePrefab, spawn.position, spawn.rotation);

            yield return new WaitForSeconds(ufoSpawnTimer);
        }
    }
    
    // Функция добавления очков к счёту
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        scoreText.text = Convert.ToString(score);
    }

    // Функция для нахождения точки появления врага, которая не была использована в прошлый раз
    Transform FindSpawnPoint()
    {
        int spawnNumber = (int)UnityEngine.Random.Range(0, spawnPoints.Length);

        if(spawnNumber == previousSpawnNumber)
        {
            while (spawnNumber == previousSpawnNumber)
            {
                spawnNumber = (int)UnityEngine.Random.Range(0, spawnPoints.Length);
            }
        }

        previousSpawnNumber = spawnNumber;

        return spawnPoints[spawnNumber];
    }

    // Перезапуск игры
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    // Функция для смены спрайтов 
    void ChangeVisualization()
    {
        visualization = !visualization;

        var damagingObjects = FindObjectsOfType<DoDamage>();    

        if(damagingObjects != null)
        {
            UnityEngine.Debug.Log(damagingObjects.Length);

            foreach (var obj in damagingObjects)
            {
                obj.ReplaceSprite();
            }
        }
    }
}
