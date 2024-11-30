using UnityEngine;

public class Roomba : MonoBehaviour
{

    //attach to enemy to randomly wander through room for checks on collisions
    public float moveSpeed = 2f; 
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseNewDirection();
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChooseNewDirection(); 
    }

    private void ChooseNewDirection()
    {
        
        float angle = Random.Range(0f, 360f);
        float radian = angle * Mathf.Deg2Rad;

        moveDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;
    }
}
