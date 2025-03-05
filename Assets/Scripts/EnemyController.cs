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
    private bool isSpawningWave = false;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    /// <summary>
    /// Spawn a wave of enemies every [timeBetweenWaves] seconds, but only if the previous wave has been defeated
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnWave()
    {
        while (true)
        {
            // Wait until all enemies are defeated before spawning the next wave
            yield return new WaitUntil(() => activeEnemies.Count == 0);

            Debug.Log($"Spawning Wave {waveNumber} with {waveNumber} enemies!");
            isSpawningWave = true;

            for (int i = 0; i < waveNumber; i++)
            {
                SpawnEnemy();
            }

            waveNumber++;  
            isSpawningWave = false;
        }
    }

    /// <summary>
    /// Spawn enemy and add it to the activeEnemies list + give it a HealthSystem component
    /// </summary>
    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            //Debug.LogError("No spawn points assigned in EnemyController!");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        activeEnemies.Add(enemy);

        enemy.GetComponent<HealthSystem>().OnDeath += () => RemoveEnemyFromList(enemy);
    }

    private void RemoveEnemyFromList(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
