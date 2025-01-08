using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorChange : MonoBehaviour
{
    public Camera m_Camera;
    public Texture2D cursorTexture;       // Base Hand
    public Texture2D cursorTexture2;      // Hand Grab
    public Texture2D cursorTexture3;      // Rock On!
    public Sprite baseSprite;
    public Sprite grabSprite;
    public Sprite devilSprite;
    public float cursorHoldDelay = 0.2f;  
    public AudioSource sourceofaudio;
    public AudioClip squelch;

    private Mouse mouse;
    private Coroutine cursorResetCoroutine;

    void Awake()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        mouse = Mouse.current;
        //Cursor.visible = false;
    }


    void Update()
    {
        //use below later for fixed cursor please!!!!
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x, cursorPos.y);

        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            SetCursorWithDelay(cursorTexture2);
        }
    }

    public void OnUIButtonPress()
    {
        // Devil Hand on ui button press
        SetCursorWithDelay(cursorTexture3);
    }

    private void SetCursorWithDelay(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);

        // Plays sound effect if you click on smth not important in the menus
        if (texture == cursorTexture2 && sourceofaudio != null && squelch != null)
        {
            sourceofaudio.PlayOneShot(squelch);
        }

        // Stops all ongoing reset coroutine to prevent conflicts
        if (cursorResetCoroutine != null)
        {
            StopCoroutine(cursorResetCoroutine);
        }

        cursorResetCoroutine = StartCoroutine(ResetCursorAfterDelay());
    }

    private IEnumerator ResetCursorAfterDelay()
    {
        yield return new WaitForSeconds(cursorHoldDelay);
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}