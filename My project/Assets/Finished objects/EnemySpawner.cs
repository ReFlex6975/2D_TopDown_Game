using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // ������ �����
    public float spawnRadius = 5f;       // ������ ������ ������ �������
    public float spawnInterval = 3f;     // �������� ����� ��������
    public int maxEnemies = 5;           // ������������ ���������� ������

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

            // ���������, ������� ������ ����������
            spawnedEnemies.RemoveAll(enemy => enemy == null); // ������� ������ �� ������������ ������

            if (spawnedEnemies.Count < maxEnemies)
            {
                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                spawnedEnemies.Add(enemy);
            }
        }
    }
}
