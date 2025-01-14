using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorChange : MonoBehaviour
{
    public Camera m_Camera;   
    public Sprite baseSprite;     // Base Hand 
    public Sprite grabSprite;     // Grab
    public Sprite devilSprite;    // Rock On!
    public float cursorHoldDelay = 0.2f;  
    public AudioSource sourceofaudio;
    public AudioClip squelch;
    private Mouse mouse;
    private Coroutine cursorResetCoroutine;
    public Image ObjectwithImage;

    void Awake()
    {
        mouse = Mouse.current;
        Cursor.visible = false;
    }

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        RectTransform canvasRect = rectTransform.parent as RectTransform;

        // Convert the screen position of the cursor to the local position within the Canvas
        Vector2 localCursorPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,               // Ref to The Canvas 
            Input.mousePosition,      // Ref to the Cursor position in screen current space
            null,                     // No camera needed since we are operating in Screen Space - Overlay 
            out localCursorPos        // Output the local position 
        );

        // Update the position of the Cursor
        rectTransform.anchoredPosition = localCursorPos;

        // Check for left mouse button press
        if (mouse != null && mouse.leftButton.wasPressedThisFrame && ObjectwithImage.sprite != devilSprite)
        {
            SetCursorWithDelay(grabSprite);
        }
    } 

    public void OnUIButtonPress()
    {
        // Set the sprite to the devil hand
        ObjectwithImage.sprite = devilSprite;

        // Stop any ongoing reset coroutine to avoid conflicts
        if (cursorResetCoroutine != null)
        {
            StopCoroutine(cursorResetCoroutine);
        }

        // Start a coroutine to reset the sprite after 1 second
        cursorResetCoroutine = StartCoroutine(ResetDevilCursorAfterDelay());
    }

    private void SetCursorWithDelay(Sprite texture)
    {
        ObjectwithImage.sprite = texture;

        // Play sound effect for the grab sprite
        if (texture == grabSprite && sourceofaudio != null && squelch != null)
        {
            sourceofaudio.PlayOneShot(squelch);
        }

        // Stop any ongoing reset coroutine to avoid conflicts
        if (cursorResetCoroutine != null)
        {
            StopCoroutine(cursorResetCoroutine);
        }

        // Start a coroutine to reset the sprite after the standard delay
        cursorResetCoroutine = StartCoroutine(ResetCursorAfterDelay());
    }

    private IEnumerator ResetCursorAfterDelay()
    {
        yield return new WaitForSeconds(cursorHoldDelay);
        ObjectwithImage.sprite = baseSprite;
    }

    private IEnumerator ResetDevilCursorAfterDelay()
    {
        yield return new WaitForSeconds(1f); // 1-second delay for the devil sprite
        ObjectwithImage.sprite = baseSprite;
    }
}
