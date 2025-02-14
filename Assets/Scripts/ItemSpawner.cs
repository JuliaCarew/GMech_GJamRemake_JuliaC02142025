using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawner : MonoBehaviour
{
    public GameObject woodPrefab;
    public GameObject rockPrefab;
    public GameObject bottlePrefab;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private Vector2Int mapSize = new Vector2Int(20,10); // map is 20x10 for now
    [SerializeField] private float spawnInterval = 3f;


    void Start()
    {
        StartCoroutine(SpawnItemCoroutine());
    }

    private IEnumerator SpawnItemCoroutine()
    {
        while(true){
            Vector2 spawnPosition;
            if (GetRandomPosition(out spawnPosition))
            {
                SpawnItem(spawnPosition);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /// <summary>
    /// Get a random position on the map.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    private bool GetRandomPosition(out Vector2 spawnPosition)
    {
        for(int i = 0; i < 10; i++){
            Vector3Int tilePosition = new Vector3Int(
                Random.Range(0, mapSize.x),
                Random.Range(0, mapSize.y),
                0
            );

            if(tilemap.GetTile(tilePosition) == floorTile){
                spawnPosition = tilemap.GetCellCenterWorld(tilePosition);
                return true;
            }
        }
        spawnPosition = Vector2.zero;
        return false;
    }

    /// <summary>
    /// Spawn a random item at the given position.
    /// </summary>
    /// <param name="spawnPosition"></param>
    private void SpawnItem(Vector2 spawnPosition)
    {
        int random = Random.Range(0, 3);
        GameObject prefabToSpawn = random switch
            {
                0 => woodPrefab,
                1 => rockPrefab,
                2 => bottlePrefab,
                _ => null
            };

            if (prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }
    }
}
