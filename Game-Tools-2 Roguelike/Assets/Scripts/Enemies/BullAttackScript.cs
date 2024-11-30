using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAttackScript : MonoBehaviour
{
    //public PlayerHealthScript playerHealthScript;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponentInParent<PlayerHealthScript>().TakeDamage(1);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponentInParent<EnemyHealthScript>().TakeDamage(1);
        }
    }
}
