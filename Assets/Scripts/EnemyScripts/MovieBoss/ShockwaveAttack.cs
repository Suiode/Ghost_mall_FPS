using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveAttack : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] Vector3 maxGrowthSize = new Vector3 (50, 50, 50);
    [SerializeField] float maxTime = 2f;
    [SerializeField] float damage = 25;
    [SerializeField] float knockbackForce;


    [Header("Objects")]
    [SerializeField] GameObject shockwaveModel;





    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShockwaveDisperse());
    }



    //Grow the edath ball until it reaches max size set in inspector
    public IEnumerator ShockwaveDisperse()
    {
        float timer = 0;

        while (timer < maxTime)
        {
            yield return null;
            timer += Time.deltaTime;

            shockwaveModel.transform.localScale = Vector3.Lerp(shockwaveModel.transform.localScale, maxGrowthSize, (timer / maxTime));
        }
        Destroy(gameObject);
    }


    //Dealing damage
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Shockwave hit someone");

        HealthSystem targetHealth = other.GetComponentInParent<HealthSystem>();
        CharacterController targetController = other.GetComponent<CharacterController>();

        if( targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        if(targetController != null)
        {
            targetController.Move((other.transform.position - gameObject.transform.position) * knockbackForce);
        }
    }

}
