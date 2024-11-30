using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpMovementScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public Vector2 inputVelocity;
    public float speed;
    public float speedModifier;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputVelocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputVelocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        Vector2 velocity = body.velocity;
        inputVelocity = -player.transform.InverseTransformPoint(transform.position).normalized;
        inputVelocity.x *= (speed * Time.deltaTime * speedModifier);
        inputVelocity.y *= (speed * Time.deltaTime * speedModifier);
        body.AddForce(inputVelocity - (velocity * 16));
    }
}
