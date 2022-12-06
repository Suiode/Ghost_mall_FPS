using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Video_options : MonoBehaviour
{
    [Header("Resolution")]
    [SerializeField] int currentRes;
    [SerializeField] int previousRes;
    private Resolution[] defaultUnityResolutions;
    public Resolution[] shortResList;
    private List<int> shortResolutionList = new List<int>();
    [SerializeField] TMP_Dropdown resolutionDropdown;


    [Header("Vsync (0 = off, 1 = on, 2 = Double Buffered)")]
    [SerializeField] int vSyncMode = 1;
    [SerializeField] TMP_Dropdown vSyncDropdown;


    [Header("Framerate")]
    [SerializeField] int framerateTarget;
    [SerializeField] TMP_InputField framerateInputField;


    [Header("Fullscreen (0 = Fullscreen, 1 = Borderless, 2 = Windowed)")]
    [SerializeField] int fullscreenMode;
    [SerializeField] TMP_Dropdown fullscreenDropdown;






    // Start is called before the first frame update
    void Start()
    {
        defaultUnityResolutions = Screen.resolutions;




        //Gather resolution dropdown menu options
        List<string> optionsList = new List<string>();
        int currentResolutionIndex = 0;
        string option;

        //Run through every resolution and only add unique entries
        for (int i = 0; i < defaultUnityResolutions.Length; i++)
        {
            option = defaultUnityResolutions[i].width + " x " + defaultUnityResolutions[i].height;

            if (!optionsList.Contains(option))
            {
                optionsList.Add(option);
                shortResolutionList.Add(i);
            }


            //Ties the shortened list to the full list from Unity
            if (defaultUnityResolutions[i].width == Screen.width && defaultUnityResolutions[i].height == Screen.height && !optionsList.Contains(defaultUnityResolutions[i].width + " x " + defaultUnityResolutions[i].height))
            {
                currentResolutionIndex = i;
                previousRes = currentResolutionIndex;
            }
        }


        resolutionDropdown.AddOptions(optionsList);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


    }


    public void ResolutionChange()
    {
        Debug.Log("Resolution change has been called");
        Resolution resolution = defaultUnityResolutions[shortResolutionList[resolutionDropdown.value]];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        //keepNewRes.SetActive(true);
        //newResValue = resolutionDropdown.value;
        //oldRes.text = "OLD:    " + resolutions[shortResolutionList[oldResValue]].width + " x " + resolutions[shortResolutionList[oldResValue]].height;
        //newRes.text = "NEW:    " + resolution.width + " x " + resolution.height;
        //Timer.maxValue = waitForUserConfirmTime;
        //Timer.value = Timer.maxValue;

    }



    public void vSyncChange()
    {
        Debug.Log("Changed vSync value to: " + vSyncDropdown.value);
        QualitySettings.vSyncCount = vSyncDropdown.value;


        //If 
        if(QualitySettings.vSyncCount == 1 || QualitySettings.vSyncCount == 2)
        {
            framerateInputField.enabled = false;
        }
        else {
            framerateInputField.enabled = true;
        }

    }


    public void FramerateChange()
    {
        Debug.Log("Changed framerate to: " + framerateInputField.text);
        Application.targetFrameRate = int.Parse(framerateInputField.text);

    }


    public void FulllscreenChange()
    {

        fullscreenMode = fullscreenDropdown.value; //0 = Fullscreen, 1 = Borderless, 2 = Windowed

        if (fullscreenMode == 0)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if(fullscreenMode == 1)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if(fullscreenMode == 2)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

    }


    public void ApplyChanges()
    {


    }
}
