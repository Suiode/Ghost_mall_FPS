using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornShooting : MonoBehaviour
{
    [SerializeField] GameObject popcornKernel;
    [SerializeField] Transform[] hands;
    [SerializeField] GameObject boss;
    public bool shooting = false;
    [SerializeField] float waitBeforeShooting;
    [SerializeField] float endTime;
    public float angleBetween = 0.0f;
    public float spawnAngleOffset = 90;





    public IEnumerator PopcornDestruction()
    {
        yield return new WaitForSeconds(waitBeforeShooting);
        float timer = 0;


        while(timer <= endTime)
        {
            yield return null;
            timer += Time.deltaTime;


            foreach (Transform hand in hands)
            {
                Vector3 targetDir = boss.transform.position - hand.transform.position;
                angleBetween = Vector3.Angle(transform.forward, targetDir);

                Instantiate(popcornKernel, hand.position, Quaternion.Euler(new Vector3(0, angleBetween + spawnAngleOffset, 0)));
            }
        }
    }
}
