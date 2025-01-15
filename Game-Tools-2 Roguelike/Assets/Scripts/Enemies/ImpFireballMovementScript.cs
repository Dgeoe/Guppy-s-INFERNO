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

    private void TestCode()
    {
        fireballTime += Time.deltaTime;
        velocity = body.velocity;
        inputVelocity = -player.transform.InverseTransformPoint(transform.position);
        float a = Mathf.Sqrt((inputVelocity.x * inputVelocity.x) + (inputVelocity.y * inputVelocity.y));
        float b = Mathf.Sqrt((fireballVelocity.x * fireballVelocity.x) + (fireballVelocity.y * fireballVelocity.y));
        Vector2 point1 = inputVelocity - fireballVelocity;
        Vector2 radiusPoint = new Vector2(a, 0);
        Vector2 point2 = radiusPoint - fireballVelocity;
        float c1 = Mathf.Sqrt((point1.x * point1.x) + (point1.y * point1.y));
        float c2 = Mathf.Sqrt((point2.x * point2.x) + (point2.y * point2.y));
        float angle1 = (Mathf.Sqrt(a) + Mathf.Sqrt(b) - Mathf.Sqrt(c1)) / (2 * (a * b)); // Angle between fireballVelocity, fireball & inputVelocity
        float angle2 = (Mathf.Sqrt(a) + Mathf.Sqrt(b) - Mathf.Sqrt(c2)) / (2 * (a * b)); // Angle between fireballVelocity, fireball & radiusPoint
        float num = fireballVelocity.y / fireballVelocity.x;
        float angle3 = Mathf.Tan(num);
    }
}
