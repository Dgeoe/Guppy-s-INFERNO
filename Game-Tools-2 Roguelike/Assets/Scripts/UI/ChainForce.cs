using UnityEngine;

public class ChainForce : MonoBehaviour
{
    public float cursorForce = 5f;    
    public float cursorRadius = 1f;   

    private Rigidbody2D rb;
    
    void Awake()
    {
        Cursor.visible = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToCursor = cursorPosition - (Vector2)transform.position;
        
        
        if (directionToCursor.magnitude < cursorRadius)
        {
            // Apply force to push the chain link away from the cursor
            Vector2 forceDirection = -directionToCursor.normalized; 
            rb.AddForce(forceDirection * cursorForce, ForceMode2D.Force);
        }
    }
}
