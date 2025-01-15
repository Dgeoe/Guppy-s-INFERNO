using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAttackScript : MonoBehaviour
{
    public BullMovementScript bullMovementScript;
    private Vector2 inputVelocity;
    //public PlayerHealthScript playerHealthScript;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponentInParent<PlayerHealthScript>().TakeDamage(1);
            bullMovementScript.Stop();
            Debug.Log("Player hit");
        }
        else if (collision.gameObject.transform.parent.gameObject.tag == "Bomb")
        {
            BombCoreScript bombCoreScript = collision.gameObject.GetComponentInParent<BombCoreScript>();
            bombCoreScript.Explode();
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponentInParent<EnemyHealthScript>().TakeDamage(1);
        }
    }
}
