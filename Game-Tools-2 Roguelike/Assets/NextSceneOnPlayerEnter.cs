using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneOnPlayerEnter : MonoBehaviour
{
    // Ensure the object has a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is tagged as "Player"
        {
            // Get the current scene index and load the next scene in the build settings
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex); // Load the next scene
            }
        }
    }
}
