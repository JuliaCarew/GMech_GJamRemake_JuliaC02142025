using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Enemy prefab
    [SerializeField] private Transform[] spawnPoints; // Possible spawn locations
    [SerializeField] private float timeBetweenWaves = 5f;

    private int waveNumber = 1;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnWave());
        // add the fact that the next wave will not spawn until current wave is defeated.
    }

    private IEnumerator SpawnWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            Debug.Log($"Spawning Wave {waveNumber} with {waveNumber} enemies!");

            for (int i = 0; i < waveNumber; i++)
            {
                SpawnEnemy();
            }

            waveNumber++; // Increase wave number
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned in EnemyController!");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        activeEnemies.Add(enemy);
    }
}
