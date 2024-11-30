using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMasker : MonoBehaviour
{
    [SerializeField] private GameObject objectToFade;
    [SerializeField] private GameObject[] objectsToDestroy;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        if (objectToFade != null)
        {
            spriteRenderer = objectToFade.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                originalColor = spriteRenderer.color;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeOut());
            StartCoroutine(DestroyObjectsAfterDelay(2f));
        }
    }

    private System.Collections.IEnumerator FadeOut()

    {

        float duration = 1f; 

        float time = 0;



        while (time < duration)

        {

            time += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, time / duration);

            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            yield return null;

        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

    }



    private System.Collections.IEnumerator DestroyObjectsAfterDelay(float delay)

    {

        yield return new WaitForSeconds(delay);



        foreach (GameObject obj in objectsToDestroy)

        {

            if (obj != null)

            {

                Destroy(obj);

            }

        }

    }

}

