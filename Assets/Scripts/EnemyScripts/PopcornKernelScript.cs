using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornKernelScript : MonoBehaviour
{
    [SerializeField] float damage = 10;
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask hittableObjects;
    [SerializeField] LayerMask ignoreLayers;




    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(this.gameObject.layer, 7, true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 rotation = transform.rotation * Vector3.up;
        //transform.localPosition += this.transform.forward * movementSpeed;

        rb.AddRelativeForce(-transform.forward * movementSpeed, ForceMode.Impulse);
    }



    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Popcorn kernel trigger hit someone");

        HealthSystem targetHealth = collision.transform.GetComponentInParent<HealthSystem>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }


        Debug.Log("Popcorn hit something");
        Destroy(this.gameObject);
    }

}
