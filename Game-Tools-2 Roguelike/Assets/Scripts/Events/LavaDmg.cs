using UnityEngine;

public class LavaDmg : MonoBehaviour
{
    public GameObject player;
    public PlayerHealthScript playerHealthScript;
    public float damageInterval = 1f; // Time between damage is dealt
    private float damageTimer = 0f;
    public Animator Mater; // Reference to the Animator

    private void Start()
    {
        // Find the player by tag and get their health script
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

        // Ensure the Animator is assigned
        if (Mater == null)
        {
            Debug.LogError("Animator not assigned in LavaDmg script!");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava"))
        {
            // Increment the timer
            damageTimer += Time.deltaTime;

            // Apply damage every specified time interval
            if (damageTimer >= damageInterval)
            {
                Debug.Log("Player hit");

                // Trigger the animation
                if (Mater != null)
                {
                    Mater.SetTrigger("On");
                }

                // Apply damage
                playerHealthScript.TakeDamage(1);

                // Reset the timer
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset the timer when the player leaves the Lava
        if (collision.CompareTag("Lava"))
        {
            damageTimer = 0f;

            // Reset the animation
            if (Mater != null)
            {
                Mater.SetTrigger("Off");
            }
        }
    }
}
