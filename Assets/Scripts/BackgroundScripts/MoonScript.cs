using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    [Header("Game Objects")]
    public Light moonlight;
    public Camera camera;


    [Header("Moon shift variables")]
    [SerializeField] private bool bloodMoonEnabled;
    [SerializeField] private float defaultChangeTime = 0.2f;
    [SerializeField] private float changeTime = 0;
    [SerializeField] private bool shiftingPhase = false;
    [SerializeField] Color oldColor;
    public Color bloodMoonColor = Color.red;
    public Color defaultColor;


    void Start()
    {
        //Change blood moon status based on startup color
        if (moonlight.color == Color.red)
        {
            StartCoroutine(MoonShift(Color.red));
            bloodMoonEnabled = true;
        }
        else
        {
            bloodMoonEnabled = false;
            StartCoroutine(MoonShift(Color.gray));
        }

        GameManager.moonController = this;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //Current setup to change color whenever I like. In the future this will all be handled by the MoonShift() coroutine
    //    //if(Input.GetKeyDown(KeyCode.F) && !bloodMoonEnabled)
    //    //{
    //    //    oldColor = moonlight.color;
    //    //    StartCoroutine(MoonShift(Color.red));
    //    //    bloodMoonEnabled = true;
    //    //}
    //    //else if(Input.GetKeyDown(KeyCode.F) && bloodMoonEnabled)
    //    //{
    //    //    oldColor = moonlight.color;
    //    //    StartCoroutine(MoonShift(Color.gray));
    //    //    bloodMoonEnabled = false;
    //    //}
    //}


    public IEnumerator MoonShift(Color newColor, float shiftTimer = 0.2f)
    {

        shiftingPhase = true;
        changeTime = 0;


        while (shiftingPhase)
        {
            yield return null;
            changeTime += Time.deltaTime;
            moonlight.color = Color.Lerp(oldColor, newColor, (changeTime / shiftTimer));
            camera.backgroundColor = Color.Lerp(oldColor, newColor, (changeTime / shiftTimer));
            

            if (changeTime >= shiftTimer)
            {
                shiftingPhase = false;
                changeTime = 0;
            }
        }
        
    }


    public void ShiftColorBloodDefault(float shiftTimer = 0.2f)
    {
        oldColor = moonlight.color;
        StartCoroutine(MoonShift(Color.red, shiftTimer));
        bloodMoonEnabled = true;
    }

    public void ShiftBackToDefault(float shiftTimer = 0.2f)
    {
        oldColor = moonlight.color;
        StartCoroutine(MoonShift(Color.gray, shiftTimer));
        bloodMoonEnabled = false;
    }
}



