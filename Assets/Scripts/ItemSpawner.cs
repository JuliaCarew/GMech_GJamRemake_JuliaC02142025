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
    //[SerializeField] private float spawnInterval = 1.3f;


    void Start()
    {
        //StartCoroutine(SpawnItemCoroutine());
        LetterSetCHAIR.SetActive(true);
    }
    public void ChairLetterSet()
    {
        Debug.Log("Item Spawner: spawned CHAIR letter set");
        LetterSetCHAIR.SetActive(true);
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
}
