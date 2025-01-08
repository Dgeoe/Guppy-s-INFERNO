using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickupScript : MonoBehaviour
{
    public BombCoreScript bombCoreScript;
    InputAction pickupAction;
    public GameObject player;
    public GameObject pickupObject;
    public Rigidbody2D body;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        pickupAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pickupAction.IsPressed())
        {
            Debug.Log("Interact");
            if (transform.childCount > 0)
            {
                Debug.Log("Drop");
                animator.SetTrigger("throw");
                BombCoreScript bombCoreScript = GetComponentInChildren<BombCoreScript>();
                Vector2 tempVector2 = new Vector2(body.velocity.x, body.velocity.y).normalized;
                transform.GetChild(0).transform.localPosition = new Vector2(tempVector2.x * 2.5f, tempVector2.y * 2.5f);
                bombCoreScript.LaunchBomb();
                transform.GetChild(0).parent = null;
                pickupAction.Disable();
            }
            else if (transform.childCount == 0 && bombCoreScript.lit == true)
            {
                Debug.Log("Pick up");
                animator.SetTrigger("pickup");
                pickupObject.transform.parent = gameObject.transform;
                pickupAction.Disable();
            }
        }
        else if (!pickupAction.IsPressed())
        {
            pickupAction.Enable();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            pickupObject = collision.gameObject;
            bombCoreScript = pickupObject.GetComponent<BombCoreScript>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pickupObject = null;
        bombCoreScript = null;
    }
}
