using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplodeScript : MonoBehaviour
{
    public PlayerHealthScript playerHealthScript;
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerHealthScript>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Bomb hits player");
            playerHealthScript.TakeDamage(1);
        }
        else if (collision.tag == "Enemy")
        {
            Debug.Log("Bomb hits enemy");
            collision.GetComponentInParent<EnemyHealthScript>().TakeDamage(1);
        }
    }
}
