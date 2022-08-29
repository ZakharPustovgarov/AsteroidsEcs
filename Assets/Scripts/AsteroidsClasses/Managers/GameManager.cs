using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ����� ��������� ����, ���������� �� �������� ����� �����������, ������� ����� � ����� ��������
public class GameManager : MonoSingleton<GameManager>
{
    // �����, � ������� ����� ��������� ����� ����
    [SerializeField]
    Transform[] spawnPoints;

    // ����
    int score;
    public int Score
    { get { return score; } }

    // �����, ����������� ���� � ���������� ������
    [SerializeField]
    Text scoreText;

    // ��� �� �����?
    public bool isPlayerAlive;

    // ������� ������ � ������ ����� �����������
    [SerializeField]
    GameObject bigAsteroidPolyPrefab, ufoPolyPrefab, bigAsteroidSpritePrefab, ufoSpritePrefab;

    // �����, ����� ������� ���������� ����������� ��� ������
    [SerializeField]
    float asteroidSpawnTimer, ufoSpawnTimer;

    // ����������, ������ ��� ����, ����� ����� �� ���������� �� ����� � ��� �� ����� ���� �� ������
    int previousSpawnNumber = -1;

    // ������� ��� �����������
    bool visualization;

    // �������� ��� ��������� �������� ���� �����������
    public bool Visualization
    { get { return visualization; } }

    //
    PlayerController player = null;

    // ��� ������ �� ���� ���� �������. ������� �� �������� �������, ������������
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag != "Laser")
        {
            GameObject.Destroy(other.gameObject);
        }
    }

    // � ������ ���� ���� �������� �� 0 � ����������� �������� ��������� ������
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

    //�������� ��������� ���
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

    //�������� ��������� ����������
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
    
    // ������� ���������� ����� � �����
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        scoreText.text = Convert.ToString(score);
    }

    // ������� ��� ���������� ����� ��������� �����, ������� �� ���� ������������ � ������� ���
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

    // ���������� ����
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    // ������� ��� ����� �������� 
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
