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


 



    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(this.gameObject.layer, 7, true);
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce((transform.forward) * movementSpeed);
        
    }



    public void OnCollisionEnter(Collision collision)
    {
        
        //Debug.Log("Popcorn kernel trigger hit: " + collision.transform.name);

        HealthSystem targetHealth = collision.transform.GetComponentInParent<HealthSystem>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        


        if(Time.time >= spawnTime + invulnTime)
        {
            Destroy(this.gameObject);
        }
        

    }

}
