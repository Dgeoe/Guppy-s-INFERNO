using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProximityAudio : MonoBehaviour
{
    public Collider2D triggerCollider; 
    public AudioSource audioSource; 
    public GameObject player; 
    public float maxVolume = 0.3f; 
    public float fadeSpeed = 0.05f; 
    private bool isPlayerNearby = false;

    void Start()
    {
        player = GameObject.Find("Player");
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Ensure the audio is looping
        audioSource.loop = true;

        // Start playing the sound but set volume to 0 initially
        audioSource.volume = 0f;
        audioSource.Play();
    }

    void Update()
    {
        if (isPlayerNearby)
        {
            // Gradually increase the volume to the maxVolume
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, maxVolume, fadeSpeed * Time.deltaTime);
        }
        else
        {
            // Gradually decrease the volume to 0
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0f, fadeSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            isPlayerNearby = false;
        }
    }
}

