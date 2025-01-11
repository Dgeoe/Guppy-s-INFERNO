using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpFireballCoreScript : MonoBehaviour
{
    public ImpFireballMovementScript impFireballMovementScript;
    public ImpCoreScript impCoreScript;
    public PlayerHealthScript playerHealthScript;
    public EnemyHealthScript enemyHealthScript;
    public Animator animator;
    public Rigidbody2D body;
    public GameObject player;
    public GameObject imp;
    public float fireballTime;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerHealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (impCoreScript.fireballActive == true)
        {
            fireballTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealthScript.TakeDamage(1);
            FireballImpactAnim();
        }
        else if (collision.gameObject.tag == "Enemy" && fireballTime >= 1)
        {
            collision.GetComponentInParent<EnemyHealthScript>().TakeDamage(1);
            FireballImpactAnim();
        }
    }
    public void ResetFireball()
    {
        fireballTime = 0;
        transform.localPosition = new Vector2(0, 0);
        impFireballMovementScript.velocity = new Vector2(0, 0);
        impFireballMovementScript.inputVelocity = new Vector2(0, 0);
        impFireballMovementScript.fireballVelocity = new Vector2(0, 0);
        impCoreScript.fireballActive = false;
        impFireballMovementScript.enabled = true;
        gameObject.SetActive(false);
    }
    public void FireballImpactAnim()
    {
        impFireballMovementScript.enabled = false;
        body.velocity = new Vector2(0, 0);
        animator.SetTrigger("impact");

        // Play the sound effect
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
