using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    public Vector2 Position {get; private set;}
    public Vector2 LookAtDirection {get; private set;}

    [SerializeField]private float speed = 5; 
    [SerializeField] private Tilemap tilemap; 
    [SerializeField] private TileBase floorTile;

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
    /// Check if the target position is walkable
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private bool IsPositionWalkable(Vector2 targetPosition)
    {
        Vector3Int targetCell = tilemap.WorldToCell(targetPosition);

        // Get the tile at the target position
        TileBase tileAtTarget = tilemap.GetTile(targetCell);

        // Check if the tile is a floor tile
        return tileAtTarget == floorTile;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyProjectile"))
        {
            Debug.Log("Player hit by an enemy!");
            HealthSystem playerHealth = GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }
}