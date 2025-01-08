using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public int health;
    public bool invincible;
    private void Start()
    {
        invincible = false;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy hit");
        if (!invincible)
        {
            Debug.Log("Damage dealt");
            health -= damage;
        }
    }

    private void Update()
    {
        /*
        if (health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        */
    }
}