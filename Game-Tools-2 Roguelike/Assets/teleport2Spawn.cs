using UnityEngine;

public class teleport2Spawn : MonoBehaviour
{
    private Vector2 originalSpawnPosition;
    private AudioSource audioSource;

    [Header("Audio Settings")]
    [Tooltip("The sound effect to play when the player is teleported.")]
    public AudioClip teleportSound;

    void Start()
    {
        // Save the player's original position when the scene starts
        originalSpawnPosition = transform.position;

        // Set up the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters a trigger collider
        if (other.CompareTag("TeleportTrigger"))
        {
            // Teleport the player to the original spawn position
            transform.position = originalSpawnPosition;

            // Play the teleport sound effect if it's set
            if (teleportSound != null)
            {
                audioSource.clip = teleportSound;
                audioSource.Play();
            }
        }
    }
}
