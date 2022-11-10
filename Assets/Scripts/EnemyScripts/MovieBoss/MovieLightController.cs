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
        forwardLight.color = defaultLightColor;
        lampLight.color = defaultLightColor;
    }




    public void ChangeColor(Color newColor)
    {
        previousLightColor = forwardLight.color;

        forwardLight.color = newColor;
        lampLight.color = newColor;
    }

    public void BackToDefault()
    {
        ChangeColor(defaultLightColor);
    }

    public void DefaultAttackColor()
    {
        ChangeColor(attackLightColor);
    }
}
