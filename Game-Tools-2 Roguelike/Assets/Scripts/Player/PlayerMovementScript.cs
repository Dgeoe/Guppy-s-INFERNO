using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [Header ("Drag and Drop")]
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public AudioSource speaker;
    private Vector2 inputVelocity;
    private Vector2 dashVelocity;
    public GameObject CooldownSprite;

    [Header("Movement Variables")]
    public float speed;
    private float speedModifier;
    private float walkModifier;
    public float sprintModifier;
    private bool isSprinting;
    //public bool isCutscene;
    private float timeCount;
    private float cooldownCount;
    public float dashTime;
    public float dashModifier;
    public float dashCooldown;
    private bool isDashing;

    [Header ("Sound Effects")]
    public AudioClip[] Footsteps;
    public AudioClip[] Dodgeroll;


    [Header ("Pausing")]
    public GameObject PauseMenu;
    public GameObject PauseButtons;
    public GameObject CursorStates;

    [Header ("For Use Later")]
    public bool isCutscene;

    InputAction moveAction;
    InputAction sprintAction;
    InputAction dashAction;
    InputAction pauseAction;

    // Start is called before the first frame update
    void Start()
    {
        isSprinting = false;
        isDashing = false;
        timeCount = 0;
        cooldownCount = dashCooldown;
        walkModifier = 1f;
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        dashAction = InputSystem.actions.FindAction("Dash");
        pauseAction= InputSystem.actions.FindAction("Pause");
        Cursor.visible = false;
    }
    void Awake()
    {
        Cursor.visible = false;
    }


    [Header("Sound Settings")]
    public float footstepDelay = 0.5f; // Delay in seconds between footstep sounds
    private float footstepTimer = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputVelocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputVelocity.x < 0)                    // Aligns sprite with direction of movement
        {
            spriteRenderer.flipX = true;
        }
        if (!isCutscene)
        {
            cooldownCount += Time.deltaTime;
            if (dashAction.IsPressed() && cooldownCount >= dashCooldown)            // Starts Dash
            {
                cooldownCount = 0;
                Debug.Log("Dash");
                isDashing = true;
                // Play random dodgeroll sound
                PlayOneShotWithVolumeBoost(Dodgeroll);
            }
            if (isDashing)                                                    // Checks to see if dash is started
            {
                timeCount += Time.deltaTime;
                animator.SetTrigger("roll");
                StartCooldown();

                if (timeCount < dashTime)                                     // Iterates through movement of dash until dashTime is up
                {
                    Vector2 velocity = body.velocity;
                    dashVelocity = inputVelocity.normalized;
                    dashVelocity.x *= (speed * dashModifier * Time.deltaTime);
                    dashVelocity.y *= (speed * dashModifier * Time.deltaTime);
                    body.AddForce((dashVelocity - (velocity * 16)) * (1/speedModifier));
                    //Debug.Log(velocity);
                }
                else                                                          // Stops dash
                {
                    timeCount = 0;
                    isDashing = false;
                }
            }
            else
            {
                if (sprintAction.IsPressed())                                  // Enables sprint speed modifier
                {
                    isSprinting = true;
                    speedModifier = sprintModifier;
                }
                else                                                           // Enables walk speed modifier
                {
                    isSprinting = false;
                    speedModifier = walkModifier;
                }
                footstepTimer ++;
                Vector2 velocity = body.velocity;
                inputVelocity = moveAction.ReadValue<Vector2>();
                Vector2 animVelocity = inputVelocity;
                inputVelocity.x *= (speed * Time.deltaTime * speedModifier);    // Movement of player
                inputVelocity.y *= (speed * Time.deltaTime * speedModifier);
                body.AddForce(inputVelocity - (velocity * 16));
                if (animVelocity != Vector2.zero && footstepTimer >= footstepDelay) // Footstep sound effects
                {
                    PlayRandomSound(Footsteps, isSprinting ? 1.2f : 1.0f);
                    footstepTimer = 0f; // Reset the timer after playing a sound
                }
                if (isSprinting)
                {
                    animator.SetTrigger("run");
                }
                else if (animVelocity != Vector2.zero)
                {
                    animator.SetTrigger("walk");                                 // Animation check for run, walk and idle
                }
                else
                {
                    animator.SetTrigger("idle");
                }

                //Pause Menu Activation Logic
                if (pauseAction.IsPressed())
                {
                    PauseButtons.SetActive(true);
                    PauseMenu.SetActive(true);
                    CursorStates.SetActive(true);

                    //fix later so pressing pause again unpauses and ypu dont have to use UI
                    //atm current method causes screen jittering
                    //if (!PauseMenu.activeSelf)
                    //{
                    //    PauseButtons.SetActive(true);
                    //    PauseMenu.SetActive(true);
                    //}
                    //else if (PauseMenu.activeSelf)
                    //{
                    //    PauseButtons.SetActive(false);
                    //    PauseMenu.SetActive(false);   
                    //}
                }
            }
        }
        else
        {
            body.velocity = new Vector2(0, 0);                           // Stops the player if there is a cutscene playing
        }
    }

    private void PlayOneShotWithVolumeBoost(AudioClip[] clip)
    {
        StartCoroutine(PlayRandomSoundLoudly(clip));
    }
    private void PlayRandomSound(AudioClip[] clips, float pitch = 1.0f)
    {
        if (clips.Length == 0) return;

        int randomIndex = Random.Range(0, clips.Length);
        speaker.pitch = pitch;
        speaker.PlayOneShot(clips[randomIndex]);
        
    }

    private IEnumerator PlayRandomSoundLoudly(AudioClip[] clips, float pitch = 1.0f)
    {

        // Save the current volume
        float originalVolume = speaker.volume;

        // Set the volume to maximum
        speaker.volume = 1f;

        int randomIndex = Random.Range(0, clips.Length);
        speaker.pitch = pitch;
        speaker.PlayOneShot(clips[randomIndex]);

        // Wait for the clip duration to complete
        yield return new WaitForSeconds(clips[randomIndex].length);

        // Reset the volume to the original value
        speaker.volume = originalVolume;
        
    }

    public void StartCooldown()
    {
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        CooldownSprite.SetActive(true);
        CooldownSprite.transform.localScale = new Vector3(1f, 0.15f, 0);
        Vector3 originalScale = CooldownSprite.transform.localScale;
        Vector3 targetScale = new Vector3(0f, 0.15f, 0f);
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            CooldownSprite.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        CooldownSprite.transform.localScale = targetScale;
        CooldownSprite.SetActive(false);
    }



}