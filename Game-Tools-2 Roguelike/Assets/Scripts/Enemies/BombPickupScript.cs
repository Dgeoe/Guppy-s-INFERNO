using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombPickupScript : MonoBehaviour
{
    [Header("Bomb Variables")]
    public CircleCollider2D killCollider;

    [Header ("Pickup Variables")]
    public SpriteRenderer spriteRenderer;
    public GameObject player;
    public Rigidbody2D playerRB;
    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (transform.root != transform)
        {
            killCollider.enabled = false;
            if (playerRB.velocity.x > 0)
            {
                spriteRenderer.flipX = false;
                transform.localPosition = new Vector2(0.15f, 1.25f);
            }
            else if (playerRB.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
                transform.localPosition = new Vector2(-0.15f, 1.25f);
            }
        }
        else if (transform.root == transform)
        {
            killCollider.enabled = true;
        }
    }
}
