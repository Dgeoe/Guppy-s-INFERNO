using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneAfterAnimations : MonoBehaviour
{
    public Animator animator; 
    public string animationName; 

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

