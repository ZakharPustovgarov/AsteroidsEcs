using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс большого астероида
public class BigAsteroid : SmallAsteroid
{
    // Массив направлений, в которые могут полететь осколки после уничтожения астероида пулями
    [SerializeField]
    Transform[] directions;

    // Префаб осколка астероида
    [SerializeField]
    GameObject smallAsteroidPolyPrefab;
    [SerializeField]
    GameObject smallAsteroidSpritePrefab;

    // Функция получения урона
    public override void TakeDamage(string damageType)
    {
        if(damageType != "Laser")// Если урон получен не от лазера, то
        {
            int count = (int)UnityEngine.Random.Range(1, 3);// Берётся случайное число от 1 до 3, которое ялвяется количеством осколоков, на которые разделится астероид при уничтожении пулями

            List<int> occupiedDirections = new List<int>();// Создаётся список, в котором будут хранится используемые направления

            for (int i = 0; i < count; i++)
            {
                SmallAsteroid asteroid;

                if(GameManager.Instance.Visualization == false) asteroid = GameObject.Instantiate(smallAsteroidPolyPrefab, this.transform.position, this.transform.rotation).GetComponent<SmallAsteroid>();// Создаётся объект осколка
                else asteroid = GameObject.Instantiate(smallAsteroidSpritePrefab, this.transform.position, this.transform.rotation).GetComponent<SmallAsteroid>();

                int directionNumber = FindFreeDirection(occupiedDirections);// Ищется свободное направление

                asteroid.speed *= 4f;// Скорость осколка умножается на 4

                occupiedDirections.Add(directionNumber);// В список занятых напрвлений добавляется выбарнное направление

                asteroid.ChangeDestination(directions[directionNumber]);// Смена направления у осколка
            }
        }

        base.TakeDamage(damageType); // Этот объект уничтожается
    }

    // Функция для нахождения свободного направления
    int FindFreeDirection(List<int> occupiedDirections)
    {
        int directionNumber = (int)UnityEngine.Random.Range(0, 11);

        if(occupiedDirections.Count == 0)// Если в списке занятых направлений нет элементов, то просто возращаем полученное число
        {
            return directionNumber;
        }
        else// Иначе берём случайное число до тех пор, пока его не будет в списке, полсе чего возвращаем его
        {
            while(occupiedDirections.Contains(directionNumber))
            {
                directionNumber = (int)UnityEngine.Random.Range(0, 11);
            }

            return directionNumber;
        }
    }
}
