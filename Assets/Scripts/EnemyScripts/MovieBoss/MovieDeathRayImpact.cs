using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieDeathRayImpact : MonoBehaviour
{
    [SerializeField] float damage = 25f;



    

    public void OnTriggerEnter(Collider other)
    {
        HealthSystem targetHealth = other.transform.GetComponentInParent<HealthSystem>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }


    }
}
