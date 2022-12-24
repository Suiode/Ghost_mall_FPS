using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornShooting : MonoBehaviour
{
    [SerializeField] GameObject popcornKernel;
    [SerializeField] Transform[] hands;
    [SerializeField] Transform boss;
    public bool shooting = false;
    [SerializeField] float waitBeforeShooting;
    [SerializeField] float endTime;
    public float angleBetween = 0.0f;
    public float spawnAngleOffset = 90;



    [Header("Variables for popcorn attack")]
    [SerializeField] float totalAttackTime = 1;
    [SerializeField] int amountOfKernels = 30;
    [SerializeField] float maxAngle = -60;
    [SerializeField] float distanceFromPlayer = 2f;





    public IEnumerator PopcornDestruction()
    {

        yield return new WaitForSeconds(waitBeforeShooting);

        

        for (int i = 1; i <= amountOfKernels; i++)
        {
            yield return null;

            float rotOffset = (Mathf.Abs(maxAngle + maxAngle) / amountOfKernels) * i;
            float spawnRotation = (boss.transform.forward.y - maxAngle);


            foreach (Transform hand in hands)
            {
                
                //Instantiate(popcornKernel, new Vector3(boss.transform.position.x, boss.transform.position.y, boss.transform.position.z), Quaternion.Euler(new Vector3(0, startDegrees + rotOffset, 0)));
                Instantiate(popcornKernel, new Vector3(hand.position.x, hand.position.y -0.5f, hand.position.z), Quaternion.Euler(new Vector3(0, boss.rotation.eulerAngles.y + ((-Mathf.Abs(maxAngle) + spawnAngleOffset) + rotOffset), 0)));

            }


        }

        
    }
}
