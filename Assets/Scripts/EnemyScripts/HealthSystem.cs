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
    public PlayerController playerController;




    void Start()
    {
        if(affectedBySlowdown)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        rb = GetComponent<Rigidbody>();

        playerController = this.gameObject.GetComponent<PlayerController>();



    }


    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;


        if (playerController != null)
        {
            playerController.TakeDamage(damageTaken);
        }

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

       if(playerController == null)
        Destroy(this.gameObject, timeToDissappear);
    }
}
