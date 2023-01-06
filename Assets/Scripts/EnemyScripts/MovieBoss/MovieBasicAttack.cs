using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieBasicAttack : MonoBehaviour
{

    [Header("Attack variables")]
    [SerializeField] float attackWaitTime = 0.5f;




    [Header("Aditional options")]
    [SerializeField] List<Collider> selfColliders;

    [Header("Random objects needed")]
    [SerializeField] LayerMask targetLayers;
    [SerializeField] GameObject smashShockwaveObj;

    [Header("You can add colliders to avoid hitting them")]
    [SerializeField] List<Collider> noDupesColliders;




    public IEnumerator NormalAttack()
    {
        yield return new WaitForSeconds(attackWaitTime);

        Instantiate(smashShockwaveObj, (transform.position - new Vector3(0, 2.8f, 0)), Quaternion.Euler(new Vector3(-90, 0, 0)));
    }
}
