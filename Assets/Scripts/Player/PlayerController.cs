using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TilemapWalkableData
{
    public Tilemap tilemap;
    public TileBase[] walkableTiles;
    public TileBase[] collisionTiles;
}

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Vector2 Position {get; private set;}
    public Vector2 LookAtDirection {get; private set;}
    public Rigidbody2D rb;

    [SerializeField]private float speed = 5; 
    [SerializeField] private float movementThreshold = 0.1f; // Threshold to prevent jittering
    [SerializeField] private TilemapWalkableData[] tilemapData;
    private Vector2 lastValidPosition; // Store the last valid position

    void Start()
    {
        // Initialize last valid position to starting position
        lastValidPosition = transform.position;
        Position = lastValidPosition;
    }

    void FixedUpdate()
    {
        ReadInput();
        if (!IsPositionWalkable(Position))
        {
            // Revert to last known valid position if current position is invalid
            Position = lastValidPosition;
        }
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
        if (IsPositionWalkable(targetPosition) && !IsPositionCollision(targetPosition))
        {
            Position = targetPosition;  
            lastValidPosition = Position; // Update last valid position
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
    /// Check if the target position is a collision tile on any of the tilemaps
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private bool IsPositionCollision(Vector2 targetPosition)
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
                System.Array.Exists(mapData.collisionTiles, tile => tile == tileAtTarget))
            {
                return true;
            }
        }
        return false;
    }
}