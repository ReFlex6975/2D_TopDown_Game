using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Префаб врага
    public float spawnRadius = 5f;       // Радиус спауна вокруг объекта
    public float spawnInterval = 3f;     // Интервал между спавнами
    public int maxEnemies = 5;           // Максимальное количество врагов

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Проверяем, сколько врагов существует
            spawnedEnemies.RemoveAll(enemy => enemy == null); // Очищаем список от уничтоженных врагов

            if (spawnedEnemies.Count < maxEnemies)
            {
                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                spawnedEnemies.Add(enemy);
            }
        }
    }
}
