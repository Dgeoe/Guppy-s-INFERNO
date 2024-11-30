using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        StartCoroutine(PlayAudioWithDelay(1f));
    }

    private IEnumerator PlayAudioWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
       
        audioSource.Play();
    }
}


