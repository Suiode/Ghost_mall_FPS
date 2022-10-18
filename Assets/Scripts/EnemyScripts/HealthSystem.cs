using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float health = 100;



    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;


        if (health <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        Destroy(this.gameObject);
    }
}
