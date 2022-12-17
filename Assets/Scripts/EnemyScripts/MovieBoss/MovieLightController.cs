using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieLightController : MonoBehaviour
{

    [Header("Lights and stuff")]
    [SerializeField] Light forwardLight;
    [SerializeField] Light lampLight;
    [SerializeField] Color attackLightColor;
    public Color defaultLightColor;
    public Color previousLightColor;



    // Start is called before the first frame update
    void Start()
    {
        //forwardLight.color = defaultLightColor;
        //lampLight.color = defaultLightColor;
    }




    public IEnumerator ChangeColor(Color newColor, float maxTimerTime = 0)
    {
        previousLightColor = forwardLight.color;


        if(maxTimerTime > 0)
        {
            float timer = 0;


            while(timer <= maxTimerTime)
            {
                yield return null; 

                timer += Time.deltaTime;

                forwardLight.color = Color.Lerp(forwardLight.color, newColor, timer/maxTimerTime);
                lampLight.color = Color.Lerp(lampLight.color, newColor, timer/maxTimerTime);
            }
        }
        else
        {
            forwardLight.color = newColor;
            lampLight.color = newColor;
        }
    }

    public void BackToDefault(float maxTimerTime = 0)
    {
         StartCoroutine(ChangeColor(defaultLightColor, maxTimerTime));
    }

    public void DefaultAttackColor(float maxTimerTime = 0)
    {
        StartCoroutine(ChangeColor(attackLightColor, maxTimerTime));
    }
}
