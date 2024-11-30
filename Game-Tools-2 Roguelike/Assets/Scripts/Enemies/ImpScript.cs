using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImpScript : MonoBehaviour
{
    public PlayerHealthScript playerHealthScript;
    public ImpFireballScript impFireballScript;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D body;
    public GameObject player;
    public GameObject fireball;
    private Vector2 inputVelocity;
    public int speed;
    public float fireballSpawnDistance;
    private int speedModifier;
    private int count = 0;
    public bool fireballActive;
    public float fireballCooldown;
    private float attackTime;
    private bool attackBool;
    // Start is called before the first frame update
    void Start()
    {
        attackBool = false;
        attackTime = 0;
        fireballActive = false;
        player = GameObject.FindWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerHealthScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputVelocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputVelocity.x < 0)                    // Aligns sprite with direction of movement
        {
            spriteRenderer.flipX = true;
        }
        if (attackBool)
        {
            body.velocity = new Vector2(0, 0);
            if (attackTime == 0 && fireballCooldown >= 4)
            {
                Debug.Log("attackAnim");
                animator.SetTrigger("attack");
                fireballCooldown = 0;
            }
            else if (attackTime >= 0.2f && fireballActive == false)
            {
                SpawnFireball();
            }
            else if (attackTime >= 0.8f)
            {
                attackBool = false;
                attackTime = 0;
                Debug.Log("Attack Time Reset");
            }
            attackTime += Time.deltaTime;
        }
        else if (impFireballScript.impact)
        {
            body.velocity = new Vector2(0, 0);
            animator.SetTrigger("death");
        }
        else if (!attackBool)
        {
            fireballCooldown += Time.deltaTime;
            switch (count)
            {
                case 0:
                    speedModifier = 0;
                    break;
                case 1:
                    speedModifier = 2;
                    break;
                case 2:
                    speedModifier = 1;
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            Vector2 velocity = body.velocity;
            inputVelocity = -player.transform.InverseTransformPoint(transform.position).normalized;
            inputVelocity.x *= (speed * Time.deltaTime * speedModifier);
            inputVelocity.y *= (speed * Time.deltaTime * speedModifier);
            body.AddForce(inputVelocity - (velocity * 16));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            count++;
            if (count == 1)
            {
                animator.SetTrigger("walk");
                //Debug.Log("walk");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (count == 4 && playerHealthScript.invincible >= 2)
            {
                playerHealthScript.playerHealth--;
                playerHealthScript.invincible = 0;
                Debug.Log("Player Hit! Health is now: " + playerHealthScript.playerHealth);
            }
            if (count >= 3 && fireballActive == false && fireballCooldown >= 4)
            {
                attackBool = true;
                attackTime = 0;
                //Debug.Log("Attack Time: " + attackTime);
                //Debug.Log("Fireball Cooldown: " + fireballCooldown);
                //Debug.Log("attack");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            count--;
            //Debug.Log(count);
        }
    }
    void SpawnFireball()
    {
        fireballActive = true;
        Vector2 fireballSpawnDirection = -player.transform.InverseTransformPoint(transform.position).normalized;
        //Debug.Log(fireballSpawnDirection);
        Vector2 fireballPosition = fireball.transform.localPosition;
        fireball.transform.localPosition = new Vector2(fireballPosition.x + (fireballSpawnDirection.x * fireballSpawnDistance), fireballPosition.y + (fireballSpawnDirection.y * fireballSpawnDistance));
        fireball.SetActive(true);
    }
}