using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornKernelScript : MonoBehaviour
{
    [SerializeField] float damage = 10;
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float invulnTime;
    [SerializeField] float spawnTime;
    [SerializeField] float deathTime = 1;
    [SerializeField] AudioSource audioSource;
    [SerializeField] PauseScript pauseScript;


 



    // Start is called before the first frame update
    void Start()
    {
        pauseScript = FindObjectOfType<PauseScript>();
        Physics.IgnoreLayerCollision(this.gameObject.layer, 7, true);
        audioSource = transform.GetComponent<AudioSource>();
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pauseScript.isPaused)
        {
            rb.AddForce((transform.forward) * movementSpeed);
        }
        
    }



    public void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("Popcorn kernel trigger hit: " + collision.transform.name);
        audioSource.Play();

        HealthSystem targetHealth = collision.transform.GetComponentInParent<HealthSystem>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        


        if(Time.time >= spawnTime + invulnTime)
        {

            Destroy(this.gameObject, deathTime);
        }
        

    }

}
