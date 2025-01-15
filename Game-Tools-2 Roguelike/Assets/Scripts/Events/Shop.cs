using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ShopItem : MonoBehaviour
{
    [Header("Shop Item Settings")]
    public int price = 10; 

    public enum ShopEffect
    {
        IncreaseHealth,
        IncreaseSpeed
    }

    [Header("Shop Settings")]
    public ShopEffect effect; // Dropdown to choose the effect in the Inspector
    public int healthIncreaseAmount = 1; // Amount to increase health (for IncreaseHealth effect)
    public float speedIncreaseAmount = 1.5f; // Speed multiplier (for IncreaseSpeed effect)
   // public float speedDuration = 10f; // Duration of the speed effect in seconds


    [Header("Audio Clips")]
    public AudioClip purchaseSound; 
    public AudioClip insufficientFundsSound; 

    private TextMeshProUGUI currencyText; // Reference to the "Cash" UI element
    private AudioSource audioSource; 

    private void Start()
    {
        // Find the "Cash" UI element in the Canvas
        GameObject cashObject = GameObject.Find("Cash");
        if (cashObject != null)
        {
            currencyText = cashObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Cash UI element not found! Make sure it exists in the scene.");
        }

        // Ensure we have an AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player entered the shop item's collider
        if (collision.CompareTag("Player"))
        {
            PlayerHealthScript healthScript = collision.GetComponent<PlayerHealthScript>();
            PlayerMovementScript movementScript = collision.GetComponent<PlayerMovementScript>();

            if (currencyText == null)
            {
                Debug.LogError("CurrencyText is not assigned or found!");
                return;
            }

            // Parse the current currency from the TextMeshProUGUI element
            int currentCurrency = int.Parse(currencyText.text);

            // Check if the player has enough currency
            if (currentCurrency >= price)
            {
                // Deduct the price from the player's currency and update the UI
                currentCurrency -= price;
                currencyText.text = currentCurrency.ToString();

                if (effect == ShopEffect.IncreaseHealth && healthScript != null)
                {
                    healthScript.Heal(healthIncreaseAmount);
                    Debug.Log("Health increased by " + healthIncreaseAmount);
                }
                else if (effect == ShopEffect.IncreaseSpeed && movementScript != null)
                {
                    movementScript.speed *= speedIncreaseAmount;
                    movementScript.sprintModifier *= speedIncreaseAmount;
                }


                // Play the purchase sound and destroy the item after
                StartCoroutine(PlaySoundAndDestroy(purchaseSound));
            }
            else
            {
                // Play the insufficient funds sound
                PlaySound(insufficientFundsSound);
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private IEnumerator PlaySoundAndDestroy(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length); // Wait for the sound to finish
        }

        Destroy(gameObject); // Destroy the shop item
    }
}
