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

        if (FPSCounterToggle.isOn)
        {
            this.gameObject.SetActive(true);
            StartCoroutine(CounterOn());
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }



    public void FPSToggle()
    {
        if(FPSCounterToggle.isOn)
        {
            fpsText.enabled = true;
            StartCoroutine(CounterOn());
        }
        else
        {
            fpsText.enabled = false;
            StopCoroutine(CounterOn());
        }
    }

    private IEnumerator CounterOn()
    {
        while (FPSCounterToggle.isOn)
        {
            yield return null;

            if (!pauseScript.isPaused)
            {
                float FPSfloat = (int)(1f / Time.smoothDeltaTime);
                fpsText.text = FPSfloat.ToString();
            }
        }
    }
}
