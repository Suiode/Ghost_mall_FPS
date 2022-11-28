using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieBigRayAttack : MonoBehaviour
{
    [SerializeField] Transform headTrans;
    [SerializeField] float thiccness;
    [SerializeField] float range;
    [SerializeField] float damage = 50;
    public bool deathRayActivated;
    [SerializeField] MovieLightController lightControls;
    [SerializeField] GameObject rayCone;





    public IEnumerator bigRayAttack(float attackTimer = 2.6f, float delay = 1.5f)
    {
        Debug.Log("Big ray attack has started");
        RaycastHit hit;
        float timer = 0;
        Renderer coneRenderer = rayCone.transform.GetComponent<Renderer>();
        Material coneMat = coneRenderer.material;
        deathRayActivated = true; //WOOOO DEATH RAY!!!!!!!


        new WaitForSeconds(delay);
        lightControls.DefaultAttackColor(2);
        coneMat.color = Color.magenta;


        while(timer < attackTimer)
        {
            timer += Time.deltaTime;
            rayCone.SetActive(true);
            


            if (Physics.SphereCast(headTrans.position, thiccness, headTrans.forward, out hit, range))
            {
                yield return null;


                HealthSystem targetHealth = hit.transform.GetComponentInParent<HealthSystem>();


                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damage);
                }
            }
        }

        deathRayActivated = false; //Booo! Keep it on!
        rayCone.SetActive(false); 
        lightControls.BackToDefault(2);

    }
}
