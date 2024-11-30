using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public PlayerMovementScript playerMovementScript;
    public Animator animator;
    public GameObject restartButton;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public Rigidbody2D body;
    public int playerHealth;
    public float invincible;

    [SerializeField] private Animator Circleanimator; 
    [SerializeField] private string CircleanimationTrigger = "Death"; 

    // Start is called before the first frame update
    void Start()
    {
        invincible = 0;
    }

    // Update is called once per frame
    void Update()
    {
        invincible += Time.deltaTime;
    }
    public void TakeDamage(int damage)
    {
        if (invincible >= 2)
        {
            playerHealth -= damage;
            invincible = 0;
            Debug.Log("Damage taken. Health is " + playerHealth);
            if (playerHealth == 0)
            {
                heart1.SetActive(false);
                playerMovementScript.enabled = false;
                body.velocity = new Vector2(0, 0);
                body.bodyType = RigidbodyType2D.Static;
                animator.SetTrigger("death");
                Circleanimator.SetTrigger(CircleanimationTrigger);
                restartButton.SetActive(true);
            }
            else if (playerHealth == 1)
            {
                heart2.SetActive(false);
            }
            else if (playerHealth == 2)
            {
                heart3.SetActive(false);
            }
            else if (playerHealth == 3)
            {
                heart4.SetActive(false);
            }
        }
    }

    public void Heal(int health)
    {
        playerHealth += health;
        if (playerHealth == 2)
        {
            heart2.SetActive(true);
        }
        if (playerHealth == 3)
        {
            heart3.SetActive(true);
        }
        if (playerHealth == 4)
        {
            heart4.SetActive(true);
        }
    }
}