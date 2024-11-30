using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonFunctions : MonoBehaviour
{
    [SerializeField] private int sceneToLoad = 0; // Scene index to load (specified in the Inspector)
    [SerializeField] private AudioClip buttonSound; // Sound effect for button press
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private Animator animator; 
    [SerializeField] private string animationTrigger; 

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    // Play the next scene in the build settings after an animation
    public void PlayNextScene()
    {
        if (animator != null && !string.IsNullOrEmpty(animationTrigger))
        {
            animator.SetTrigger(animationTrigger);
            StartCoroutine(WaitForAnimationAndLoadNextScene());
        }
        else
        {
            Debug.LogWarning("Animator or animation trigger not assigned! Loading scene immediately.");
            LoadNextScene();
        }
    }

    // Coroutine to wait for the animation to finish
    private System.Collections.IEnumerator WaitForAnimationAndLoadNextScene()
    {
        yield return new WaitForSeconds(2.9f); // Wait for animation duration
        LoadNextScene();
    }

    // Load the next scene
    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes to load!");
        }
    }

    // Play a specific scene based on the inspector value
    public void PlaySpecifiedScene()
    {
        if (sceneToLoad >= 0 && sceneToLoad < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Invalid scene index specified!");
        }
    }

    // Play a sound effect
    public void PlaySoundEffect()
    {
        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip not assigned!");
        }
    }
}
