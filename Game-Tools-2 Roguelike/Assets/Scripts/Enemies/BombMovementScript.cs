using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BombMovementScript : MonoBehaviour
{
    public BombCoreScript bombCoreScript;
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public int ranNum;
    public List<int> directionNum;
    public Vector2 bombVelocity;
    private Vector2 direction;
    private float moveTimer;
    // Start is called before the first frame update
    void Start()
    {
        ChangeDirection();
        directionNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bombVelocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (bombVelocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        moveTimer += Time.deltaTime;
        if (moveTimer > 3)
        {
            ChangeDirection();
        }
        Vector2 velocity = body.velocity;
        bombVelocity = direction;
        bombVelocity.x *= (speed * Time.deltaTime);
        bombVelocity.y *= (speed * Time.deltaTime);
        body.AddForce(bombVelocity - (velocity * 16));
    }
    private void ChangeDirection()
    {
        if (directionNum.Count <= 0)
        {
            directionNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        }
        ranNum = Random.Range(0, directionNum.Count);
        bombCoreScript.animationChange();
        switch (directionNum[ranNum])
        {
            case 1:
                direction = new Vector2(0, 1);
                break;
            case 2:
                direction = new Vector2(0.5f, Mathf.Sqrt(3) / 2);
                break;
            case 3:
                direction = new Vector2(Mathf.Sqrt(3) / 2, 0.5f);
                break;
            case 4:
                direction = new Vector2(1, 0);
                break;
            case 5:
                direction = new Vector2(Mathf.Sqrt(3) / 2, -0.5f);
                break;
            case 6:
                direction = new Vector2(0.5f, -Mathf.Sqrt(3) / 2);
                break;
            case 7:
                direction = new Vector2(0, -1);
                break;
            case 8:
                direction = new Vector2(-0.5f, -Mathf.Sqrt(3) / 2);
                break;
            case 9:
                direction = new Vector2(-Mathf.Sqrt(3) / 2, -0.5f);
                break;
            case 10:
                direction = new Vector2(-1, 0);
                break;
            case 11:
                direction = new Vector2(-Mathf.Sqrt(3) / 2, 0.5f);
                break;
            case 12:
                direction = new Vector2(-0.5f, Mathf.Sqrt(3) / 2);
                break;
            }
            directionNum.RemoveAt(ranNum);
            bombVelocity = new Vector2(0, 0);
            moveTimer = 0;
        }
    }
