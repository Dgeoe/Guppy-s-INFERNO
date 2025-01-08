using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpCoreScript : MonoBehaviour
{
    public ImpMovementScript impMovementScript;
    public PlayerHealthScript playerHealthScript;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D body;
    public Animator animator;
    public GameObject player;
    public GameObject fireball;
    public int count;
    public float fireballCooldown;
    public float attackTime;
    public float fireballSpawnDistance;
    public bool fireballActive;
    public bool attackBool;
    public AudioSource audioSource;

    //0= shoot, 1= death, 2=scream
    public AudioClip[] Sounds;

    [Header("Coin Drop Settings")]
    public GameObject goldCoinPrefab;
    public GameObject silverCoinPrefab;
    public GameObject bronzeCoinPrefab;

    [Range(0, 100)] public int goldProbability = 10;
    [Range(0, 100)] public int silverProbability = 40;

    // Start is called before the first frame update
    void Start()
    {
        fireballCooldown = 4;
        player = GameObject.FindWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerHealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        fireballCooldown += Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (count >= 3 && fireballActive == false && fireballCooldown >= 4)
        {
            StopImpMovement();
            AttackAnim();
            WalkAnim();
            attackBool = true;
            attackTime = 0;
        }
        if (count >= 4 && playerHealthScript.invincible == 2)
        {
            playerHealthScript.invincible = 0;
            playerHealthScript.TakeDamage(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            count++;
            //Debug.Log("Count Up: " + count);
            if (count >= 1)
            {
                impMovementScript.enabled = true;
                WalkAnim();
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(Sounds[3]);
                }
            }
            if (count >= 2)
            {
                impMovementScript.speedModifier = 1;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            count--;
            //Debug.Log("Count Down: " + count);
            if (count <= 0)
            {
                StopImpMovement();
                IdleAnim();
            }
            if (count <= 1)
            {
                impMovementScript.speedModifier = 2;
            }
        }
    }
    public void StopImpMovement()
    {
        body.velocity = new Vector2(0, 0);
        impMovementScript.enabled = false;
    }
    public void StartImpMovement()
    {
        impMovementScript.enabled = true;
    }
    public void SpawnFireball()
    {
        fireballActive = true;
        Vector2 fireballSpawnDirection = -player.transform.InverseTransformPoint(transform.position).normalized;
        //Debug.Log(fireballSpawnDirection);
        Vector2 fireballPosition = fireball.transform.localPosition;
        fireball.transform.localPosition = new Vector2(fireballPosition.x + (fireballSpawnDirection.x * fireballSpawnDistance), fireballPosition.y + (fireballSpawnDirection.y * fireballSpawnDistance));
        fireball.SetActive(true);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(Sounds[0]);
        }
    }
    public void DestorySelf()
    {
        Object.Destroy(gameObject);
    }
    public void IdleAnim()
    {
        animator.SetTrigger("idle");
    }
    public void WalkAnim()
    {
        animator.SetTrigger("walk");
    }
    public void AttackAnim()
    {
        fireballCooldown = 0;
        animator.SetTrigger("attack");
    }
    public void DeathAnim()
    {
        StopImpMovement();
        animator.SetTrigger("death");
        if (audioSource != null)
        {
            audioSource.PlayOneShot(Sounds[1]);
        }

        // Drop a coin
        DropCoin();
    }

    private void DropCoin()
    {
        // Calculate drop probabilities
        int randomValue = Random.Range(0, 100);
        GameObject coinToDrop = null;

        if (randomValue < goldProbability)
        {
            coinToDrop = goldCoinPrefab;
        }
        else if (randomValue < goldProbability + silverProbability)
        {
            coinToDrop = silverCoinPrefab;
        }
        else
        {
            coinToDrop = bronzeCoinPrefab;
        }

        // Instantiate the selected coin prefab at the enemy's position
        if (coinToDrop != null)
        {
            Instantiate(coinToDrop, transform.position, Quaternion.identity);
        }
    }
}
