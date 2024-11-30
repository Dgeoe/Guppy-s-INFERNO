using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public float moveDistance = 1f; // Distance moved per step
    public float moveSpeed = 5f; // Speed of movement
    private Vector2 moveDirection = Vector2.zero; 
    private Rigidbody2D rb;

    private bool isMoving = false; 
    private Transform player; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 targetPosition = rb.position + moveDirection * moveDistance;
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);

            if (Vector2.Distance(rb.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            CalculateDirectionToPlayer();
        }
    }

    private void CalculateDirectionToPlayer()
    {
        if (player == null) return;

        Vector2 playerPos = player.position;
        Vector2 currentPos = transform.position;

        // Determine best direction to move
        Vector2 direction = Vector2.zero;
        float deltaX = playerPos.x - currentPos.x;
        float deltaY = playerPos.y - currentPos.y;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            direction = deltaX > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            direction = deltaY > 0 ? Vector2.up : Vector2.down;
        }

        moveDirection = direction;
        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Body"))
        {
            Debug.Log("Snake hit something!");
        }
    }
}
