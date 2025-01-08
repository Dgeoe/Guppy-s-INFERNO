using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coin : MonoBehaviour
{
    [Header("How Much?")]
    //howmuch the coin is worth 
    public int Value = 1;

    [Header("References")]
    public AudioSource audioSource;
    public AudioClip PickUpSound;
    private TMP_Text CashText;
    private bool isCollected = false;
    private static int totalScore = 0; // Keep track of the total score

    private void Start()
    {
        // Find the Text object in the scene
        GameObject cashObject = GameObject.Find("Cash");
        if (cashObject != null)
        {
            CashText = cashObject.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogError("TMP_Text component not found! Find it please!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected && collision.CompareTag("Player"))
        {
            isCollected = true; // Ensure the coin isn't collected multiple times
            totalScore += Value;

            // Update the UI Text
            if (CashText != null)
            {
                CashText.text = "" + totalScore;
            }

            // Play the sound effect
            if (audioSource != null && PickUpSound != null)
            {
                audioSource.PlayOneShot(PickUpSound);
            }

            // Destroy the coin after the sound effect finishes
            float destroyDelay = PickUpSound != null ? PickUpSound.length : 0f;
            Destroy(gameObject, destroyDelay);
        }
    }
}

