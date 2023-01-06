using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSCounter : MonoBehaviour
{

    public TextMeshProUGUI fpsText;
    public Toggle FPSCounterToggle;
    private PauseScript pauseScript;


    // Start is called before the first frame update
    void Start()
    {
        pauseScript = FindObjectOfType<PauseScript>();

        //if(PlayerPrefs.GetInt("FPSCounter") < 0 || PlayerPrefs.GetInt("FPSCounter") > 1 )
        //{
        //    Debug.Log("Player Prefs is out of range. This is what it's set to: " + PlayerPrefs.GetInt("FPSCounter"));
        //    PlayerPrefs.SetInt("FPSCounter", 1);
        //    StartCoroutine(CounterOn());
        //}
        //else
        //{
        //    Debug.Log("Player Prefs is reporting a normal number. This is what it's set to: " + PlayerPrefs.GetInt("FPSCounter"));
        //    FPSToggle();
        //}


        //if (PlayerPrefs.GetInt("FPSCounter") == 0)
        //{
        //    FPSCounterToggle.isOn = false;
        //    FPSToggle();
        //    //this.gameObject.SetActive(false);
        //}
        //else if (PlayerPrefs.GetInt("FPSCounter") == 1)
        //{
        //    //this.gameObject.SetActive(true);
        //    FPSCounterToggle.isOn = false;
        //    StartCoroutine(CounterOn());
        //}

    }

    public void OnEnable()
    {

        if (PlayerPrefs.GetInt("FPSCounter") == 0)
        {
            FPSCounterToggle.isOn = false;
            FPSToggle();
            //this.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("FPSCounter") == 1)
        {
            //this.gameObject.SetActive(true);
            FPSCounterToggle.isOn = true;
            FPSToggle();
        }
        else
        {
            Debug.Log("Player Prefs is out of range. This is what it's set to: " + PlayerPrefs.GetInt("FPSCounter"));
            PlayerPrefs.SetInt("FPSCounter", 1);
            StartCoroutine(CounterOn());
        }



        

    }



    public void FPSToggle()
    {
        if(FPSCounterToggle.isOn)
        {
            fpsText.enabled = true;
            PlayerPrefs.SetInt("FPSCounter", 1);
            StartCoroutine(CounterOn());
        }
        else
        {
            fpsText.enabled = false;
            PlayerPrefs.SetInt("FPSCounter", 0);
            StopCoroutine(CounterOn());
        }
    }

    private IEnumerator CounterOn()
    {
        while (FPSCounterToggle.isOn)
        {
            yield return null;

            float FPSfloat = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = FPSfloat.ToString();
        }
    }
}
