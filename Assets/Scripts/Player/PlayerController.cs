using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TilemapWalkableData
{
    public Tilemap tilemap;
    public TileBase[] walkableTiles;
    public TileBase[] obstacleTiles;
}

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Vector2 Position {get; private set;}
    public Vector2 LookAtDirection {get; private set;}

    [SerializeField]private float speed = 5; 
    [SerializeField] private TilemapWalkableData[] tilemapData;

    void Update()
    {
        ReadInput();
        transform.position = Position;
    }

    private void ReadInput()
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) moveDirection += Vector2.up;
        if (Input.GetKey(KeyCode.S)) moveDirection += Vector2.down;
        if (Input.GetKey(KeyCode.A)) moveDirection += Vector2.left;
        if (Input.GetKey(KeyCode.D)) moveDirection += Vector2.right;

        if (moveDirection != Vector2.zero)
        {
            LookAtDirection = moveDirection;
        }

        Vector2 targetPosition = Position + moveDirection.normalized * speed * Time.deltaTime;

        // Check if the target position is floor 
        if (IsPositionWalkable(targetPosition))
        {
            Position = targetPosition;  
        }
    }

    /// <summary>
    /// Check if the target position is walkable on any of the tilemaps
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private bool IsPositionWalkable(Vector2 targetPosition)
    {
        // Iterate through all tilemaps
        foreach (var mapData in tilemapData)
        {
            if (mapData.tilemap == null) continue; // Skip null tilemaps

            Vector3Int targetCell = mapData.tilemap.WorldToCell(targetPosition);

            // Get the tile at the target position
            TileBase tileAtTarget = mapData.tilemap.GetTile(targetCell);

            // Check if the tile is in the walkable tiles array for this tilemap
            if (tileAtTarget != null && 
                System.Array.Exists(mapData.walkableTiles, tile => tile == tileAtTarget))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Check if the target position is an obstacle tile on any of the tilemaps (have this call unique puzzle word)
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private bool IsPositionObstacle(Vector2 targetPosition)
    {
        // Iterate through all tilemaps
        foreach (var mapData in tilemapData)
        {
            if (mapData.tilemap == null) continue; // Skip null tilemaps

            Vector3Int targetCell = mapData.tilemap.WorldToCell(targetPosition);

            // Get the tile at the target position
            TileBase tileAtTarget = mapData.tilemap.GetTile(targetCell);

            // Check if the tile is in the collision tiles array for this tilemap
            if (tileAtTarget != null && 
                System.Array.Exists(mapData.obstacleTiles, tile => tile == tileAtTarget))
            {
                return true;
            }
        }
        return false;
    }
}