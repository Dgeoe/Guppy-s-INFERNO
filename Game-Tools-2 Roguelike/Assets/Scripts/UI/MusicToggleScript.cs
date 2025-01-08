using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggleScript : MonoBehaviour
{
    public AudioSource musicSource;

    private bool isMusicPlaying = true;
    private List<AudioSource> otherAudioSources = new List<AudioSource>();

    void Start()
    {
        // Ensure the AudioSource is assigned
        if (musicSource == null)
        {
            Debug.LogError("No AudioSource assigned to MusicToggleScript!");
        }

        // Find all AudioSources in the scene except the one assigned
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            if (source != musicSource)
            {
                otherAudioSources.Add(source);
            }
        }
    }

    // Function to disable music
    public void DisableMusic()
    {
        if (musicSource != null && isMusicPlaying)
        {
            musicSource.Pause();
            isMusicPlaying = false;
        }
    }

    // Function to re-enable music
    public void EnableMusic()
    {
        if (musicSource != null && !isMusicPlaying)
        {
            musicSource.Play();
            isMusicPlaying = true;
        }
    }

    // Function to disable all other audio sources 
    public void DisableAllOtherAudio()
    {
        foreach (AudioSource source in otherAudioSources)
        {
            source.enabled = false; 
        }
    }

    // Function to enable all other audio sources 
    public void EnableAllOtherAudio()
    {
        foreach (AudioSource source in otherAudioSources)
        {
            source.enabled = true; 
        }
    }
}
