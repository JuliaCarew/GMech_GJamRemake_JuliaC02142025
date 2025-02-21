using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    public Vector2 Position {get; private set;}
    public Vector2 LookAtDirection {get; private set;}

    [SerializeField]private float speed = 5; 

    void Update()
    {
        ReadInput();
        transform.position = Position;
    }

    private void ReadInput()
    {
        player.transform.position = Position;
        Vector2 moveDirection = Vector2.zero;

        if(Input.GetKey(KeyCode.W)){
            moveDirection += Vector2.up;
        }
        if(Input.GetKey(KeyCode.S)){
            moveDirection += Vector2.down;
        }
        if(Input.GetKey(KeyCode.A)){
            moveDirection += Vector2.left;
        }
        if(Input.GetKey(KeyCode.D)){
            moveDirection += Vector2.right;
        }

        if(moveDirection != Vector2.zero){
            LookAtDirection = moveDirection;
        }
        Position += moveDirection.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyProjectile"))
        {
            GetComponent<HealthSystem>().TakeDamage(1);
        }
    }
}
