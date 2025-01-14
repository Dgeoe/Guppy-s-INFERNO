using System.Collections;
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

    [Header("Sound Effects")]
    int Big;
    public AudioSource speaker;
    public AudioClip DeathSad;
    public AudioClip HealHappy;

    [SerializeField] private Animator Circleanimator;
    [SerializeField] private string CircleanimationTrigger = "Death";

    // Start is called before the first frame update
    void Start()
    {
        invincible = 0;
        Big = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        invincible += Time.deltaTime;

        if (playerHealth < Big)
        {
            Debug.Log("Ouch");
            PlayOneShotWithVolumeBoost(DeathSad);
            Big = playerHealth;
        }
        else if (playerHealth > Big)
        {
            Debug.Log("Yipee");
            PlayOneShotWithVolumeBoost(HealHappy);
            Big = playerHealth;
        }
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
                body.velocity = Vector2.zero;
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

    private void PlayOneShotWithVolumeBoost(AudioClip clip)
    {
        StartCoroutine(BoostVolumeAndPlay(clip));
    }

    private IEnumerator BoostVolumeAndPlay(AudioClip clip)
    {
        // Save the current volume
        float originalVolume = speaker.volume;

        // Set the volume to maximum
        speaker.volume = 1f;

        // Play the one-shot audio
        speaker.PlayOneShot(clip);

        // Wait for the clip duration to complete
        yield return new WaitForSeconds(clip.length);

        // Reset the volume to the original value
        speaker.volume = originalVolume;
    }
}
