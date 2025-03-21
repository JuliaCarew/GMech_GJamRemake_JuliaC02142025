using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawner : MonoBehaviour
{
    public GameObject A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z;
    public GameObject LetterSetCHAIR, LetterSetSHIRT, LetterSetLIGHT;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private Vector2Int mapSize = new Vector2Int(20,10); // map is 20x10 for now
    [SerializeField] private float spawnInterval = 1.3f;


    void Start()
    {
        //StartCoroutine(SpawnItemCoroutine());
        LetterSetCHAIR.SetActive(true);
    }

    private IEnumerator SpawnItemCoroutine()
    {
        while (true)
        {
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
        for (int i = 0; i < 10; i++) 
        {
            int x = Random.Range(-mapSize.x / 2, mapSize.x / 2); // Centered X range
            int y = Random.Range(-mapSize.y / 2, mapSize.y / 2); // Centered Y range

            Vector3Int tilePosition = new Vector3Int(x, y, 0);

            if (tilemap.GetTile(tilePosition) == floorTile) // Check if valid floor tile
            {
                spawnPosition = tilemap.GetCellCenterWorld(tilePosition);
                return true;
            }
        }
        spawnPosition = Vector2.zero;
        return false;
    }
    // NOTE: change this to spawn at set spawnpoint positions instead of random positions (or create level design with set spawnpoints)

    /// <summary>
    /// Spawn a random item at the given position.
    /// </summary>
    /// <param name="spawnPosition"></param>
    private void SpawnItem(Vector2 spawnPosition)
    {
        int random = Random.Range(0, 26);
        GameObject prefabToSpawn = random switch
            {
                0 => A,
                1 => B,
                2 => C,
                3 => D,
                4 => E,
                5 => F,
                6 => G,
                7 => H,
                8 => I,
                9 => J,
                10 => K,
                11 => L,
                12 => M,
                13 => N,
                14 => O,
                15 => P,
                16 => Q,
                17 => R,
                18 => S,
                19 => T,
                20 => U,
                21 => V,
                22 => W,
                23 => X,
                24 => Y,
                25 => Z,
                _ => null
            };

            if (prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                Debug.Log("Spawned item: " + prefabToSpawn.name);
            }
    }
}
