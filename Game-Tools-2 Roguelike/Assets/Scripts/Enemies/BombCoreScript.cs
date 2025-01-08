using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using UnityEngine;

public class BombCoreScript : MonoBehaviour
{
    public BombMovementScript bombMovementScript;
    public EnemyHealthScript enemyHealthScript;
    public PlayerMovementScript playerMovementScript;
    public Animator animator;
    public Rigidbody2D body;
    public CircleCollider2D explodeCollider;
    public int throwSpeed;
    public bool lit;
    private Vector2 launchVelocity;
    private Vector2 inputVelocity;
    private GameObject player;
    private bool launched;
    private float launchTimer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerMovementScript = player.GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealthScript.health == 0 && lit == false)
        {
            LightBomb();
        }
        else if (enemyHealthScript.health < 0)
        {
            Explode();
        }
        if (launched)
        {
            launchTimer += Time.deltaTime;
            Vector2 velocity = body.velocity;
            inputVelocity = launchVelocity;
            inputVelocity.x *= (throwSpeed * Time.deltaTime);
            inputVelocity.y *= (throwSpeed * Time.deltaTime);
            body.AddForce(inputVelocity - (velocity * 16));
            if (launchTimer >= 2)
            {
                Explode();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (launched)
        {
            Explode();
        }
    }
    public void LightBomb()
    {
        lit = true;
        bombMovementScript.bombVelocity = Vector2.zero;
        bombMovementScript.enabled = false;
        body.velocity = Vector2.zero;
        animator.SetTrigger("light");
    }
    public void LaunchBomb()
    {
        launched = true;
        launchVelocity = playerMovementScript.moveAction.ReadValue<Vector2>().normalized;
        enemyHealthScript.invincible = true;
    }
    public void Explode()
    {
        launched = false;
        body.velocity = Vector2.zero;
        animator.SetTrigger("explode");
        explodeCollider.enabled = true;
        Debug.Log("Collider enabled");
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void animationChange()
    {
        var animNum = bombMovementScript.ranNum;
        switch (bombMovementScript.directionNum[animNum])
        {
            case 1:
                animator.SetTrigger("up");
                break;
            case 2:
                animator.SetTrigger("up");
                break;
            case 3:
                animator.SetTrigger("side");
                break;
            case 4:
                animator.SetTrigger("side");
                break;
            case 5:
                animator.SetTrigger("side");
                break;
            case 6:
                animator.SetTrigger("down");
                break;
            case 7:
                animator.SetTrigger("down");
                break;
            case 8:
                animator.SetTrigger("down");
                break;
            case 9:
                animator.SetTrigger("side");
                break;
            case 10:
                animator.SetTrigger("side");
                break;
            case 11:
                animator.SetTrigger("side");
                break;
            case 12:
                animator.SetTrigger("up");
                break;
        }
    }
}
