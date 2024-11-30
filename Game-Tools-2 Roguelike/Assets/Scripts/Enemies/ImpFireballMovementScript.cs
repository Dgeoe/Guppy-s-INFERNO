using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpFireballMovementScript : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject player;
    public GameObject fireballVelocityObject;
    public Vector2 velocity;
    public Vector2 inputVelocity;
    public Vector2 fireballVelocity;
    public float fireballTime;
    public float turnSpeed;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fireballTime += Time.deltaTime;
        velocity = body.velocity;
        inputVelocity = -player.transform.InverseTransformPoint(transform.position);
        fireballVelocity.x = Mathf.MoveTowards(fireballVelocity.x, inputVelocity.x, turnSpeed * Mathf.Abs(inputVelocity.x));
        fireballVelocity.y = Mathf.MoveTowards(fireballVelocity.y, inputVelocity.y, turnSpeed * Mathf.Abs(inputVelocity.y));
        fireballVelocityObject.transform.localPosition = fireballVelocity;
        Vector2 moveVelocity = fireballVelocity.normalized;
        moveVelocity.x *= (speed * Time.deltaTime);
        moveVelocity.y *= (speed * Time.deltaTime);
        body.AddForce(moveVelocity - (velocity * 16));
    }
}
