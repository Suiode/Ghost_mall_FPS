using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health = 100;

    [Header("If null, destroys the object holding this script")]
    [SerializeField] GameObject parent;

    [Header("If true, death of this object will slow down time for others that can be affected")]
    public bool affectedBySlowdown;
    [SerializeField] GameManager gameManager;
    [SerializeField] Rigidbody rb;
    [SerializeField] float timeToDissappear;




    void Start()
    {
        if(affectedBySlowdown)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        rb = GetComponent<Rigidbody>();
    }


    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;

        //Debug.Log("This object: " + transform.name + " took this much damage: " + damageTaken);

        if (health <= 0)
        {

            if (affectedBySlowdown)
                gameManager.StartSlowDown();


            Die();
        }
    }



    void Die()
    {
       if(rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
        }

        Destroy(this.gameObject, timeToDissappear);
    }
}
