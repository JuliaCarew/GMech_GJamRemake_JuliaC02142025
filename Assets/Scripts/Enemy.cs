using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private int damageToPlayer = 1;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }
        }
    }
}