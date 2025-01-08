using UnityEngine;

public class LavaDmg : MonoBehaviour
{
    public GameObject player;
    public PlayerHealthScript playerHealthScript;

    public float damageInterval = 1f; // Time between damage is dealt 
    private float damageTimer = 0f;

    private void Start()
    {
        // Taken from Seamus's imp fireball script on getting and applying damage to the player
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealthScript = player.GetComponent<PlayerHealthScript>();
        }

        // Ensure the player health script is assigned
        if (playerHealthScript == null)
        {
            Debug.LogError("PlayerHealth script not found on the Player GameObject !(Lava Script)!");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava"))
        {
            // Increment timer please
            damageTimer += Time.deltaTime;

            // Apply damage every specified time interval 
            if (damageTimer >= damageInterval)
            {
                Debug.Log("Player hit");
                playerHealthScript.TakeDamage(1);
                damageTimer = 0f; // Reset the timer
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset the timer when the player leaves the Lava 
        if (collision.CompareTag("Lava"))
        {
            damageTimer = 0f;
        }
    }
}

