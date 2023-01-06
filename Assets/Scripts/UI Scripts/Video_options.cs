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
    private List<int> shortResolutionList = new List<int>();
    private List<string> optionsList = new List<string>();
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

    [Header("Game Manager")]
    GameManager gameManager;




    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }




        //Set fullscreen dropdown to value at startup
        if(Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
        {
            fullscreenDropdown.value = 0;
        }
        else if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            fullscreenDropdown.value = 1;
        }
        else if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            fullscreenDropdown.value = 2;
        }


        
    }


    //When enabled, check the current resolution and set the drop down to that
    public void OnEnable()
    {


        //Set framerate to the current selected at startup
        //framerateInputField.text = framerateTarget.ToString();
        framerateInputField.text = Application.targetFrameRate.ToString();
        FramerateChange();

        //Set vSync to the current selected at startup
        vSyncDropdown.value = QualitySettings.vSyncCount;
        vSyncChange();




        //Run through every resolution and only add unique entries
        defaultUnityResolutions = Screen.resolutions;

        int currentResolutionIndex = 0;
        string option;


        for (int i = 0; i < defaultUnityResolutions.Length; i++)
        {

            option = defaultUnityResolutions[i].width + " x " + defaultUnityResolutions[i].height;


            if (!optionsList.Contains(option))
            {
                optionsList.Add(option);
                shortResolutionList.Add(i);
            }

            if (defaultUnityResolutions[i].width == Screen.width && defaultUnityResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = shortResolutionList.Count - 1;
                Debug.Log("This is the current resolution: " + currentResolutionIndex + " ," + defaultUnityResolutions[shortResolutionList[currentResolutionIndex]]);
            }
        }


        resolutionDropdown.options.Clear();
        resolutionDropdown.AddOptions(optionsList);
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.SetValueWithoutNotify(currentResolutionIndex);


        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();





        Start();
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
        Debug.Log("Resolution being set is: " + defaultUnityResolutions[shortResolutionList[resolutionDropdown.value]] + " Unity's internal resolution is: " + Screen.currentResolution + " width is: " + Screen.width + " and height is: " + Screen.height);

    }



    public void vSyncChange()
    {
        Debug.Log("Changed vSync value to: " + vSyncDropdown.value);

        //framerateTarget = int.Parse(framerateInputField.text);

        QualitySettings.vSyncCount = vSyncDropdown.value;
        




        //Toggle based on current settings
        if (QualitySettings.vSyncCount == 1 || QualitySettings.vSyncCount == 2)
        {
            //framerateTarget = int.Parse(framerateInputField.text);
            framerateInputField.text = framerateTarget.ToString();
            framerateInputField.interactable = false;
            


            //Show the current framerate when vsync is enabled
            if (vSyncDropdown.value == 2)
            {
                framerateInputField.text = (Screen.currentResolution.refreshRate/2).ToString();
            }
            else if(vSyncDropdown.value == 1)
            {
                framerateInputField.text = Screen.currentResolution.refreshRate.ToString();
            }
        }
        else 
        {
            framerateInputField.interactable = true;
            framerateInputField.text = framerateTarget.ToString();
            FramerateChange();
        }

    }
   


    public void FramerateChange()
    {
        Debug.Log("Changed framerate to: " + framerateInputField.text);

        if (framerateInputField.interactable)
        {
            framerateTarget = int.Parse(framerateInputField.text);
        }

        Application.targetFrameRate = int.Parse(framerateInputField.text);
    }


    public void FulllscreenChange()
    {

        Debug.Log("Changed fullscreen mode: " + Screen.fullScreenMode);

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
