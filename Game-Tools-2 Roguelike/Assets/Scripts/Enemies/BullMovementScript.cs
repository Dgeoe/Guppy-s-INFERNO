using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullMovementScript : MonoBehaviour
{
    public BullCoreScript bullCoreScript;
    public GameObject player;
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Vector2 inputVelocity;
    public float speed;
    public float speedModifier;
    public float moveX;
    public float moveY;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        player = GameObject.FindWithTag("Player");
        inputVelocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        Vector2 velocity = body.velocity;
        //Debug.Log(velocity);
        inputVelocity = new Vector2(moveX, moveY);
        inputVelocity.x *= (speed * Time.deltaTime * speedModifier);
        inputVelocity.y *= (speed * Time.deltaTime * speedModifier);
        body.AddForce(inputVelocity - (velocity * 16));
    }
    private void Update()
    {
        if (body.velocity == Vector2.zero && timer >= 0.25f)
        {
            moveX = 0;
            moveY = 0;
            timer = 0;
            animator.SetTrigger("crash");
            //bullCoreScript.isCharging = false;
            this.enabled = false;
        }
    }
}
