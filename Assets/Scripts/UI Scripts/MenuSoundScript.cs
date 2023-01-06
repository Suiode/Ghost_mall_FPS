using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MenuSoundScript : MonoBehaviour
{
    //UI Components
    [Header("Mixer inputs")]
    [SerializeField] float masterVolume;
    [SerializeField] Slider masterSlider;
    [SerializeField] TMP_InputField masterInput;
    [SerializeField] float effectsVolume;
    [SerializeField] Slider effectsSlider;
    [SerializeField] TMP_InputField effectsInput;
    [SerializeField] float musicVolume;
    [SerializeField] Slider musicSlider;
    [SerializeField] TMP_InputField musicInput;
    [SerializeField] float minimumSoundInput;

    [Header("Mixers")]
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] AudioMixerGroup effectsMixer;
    [SerializeField] AudioMixerGroup musicMixer;



    // Start is called before the first frame update
    void Start()
    {
        effectsMixer = masterMixer.FindMatchingGroups("Effect")[0];
        musicMixer = masterMixer.FindMatchingGroups("Music")[0];

        MasterVolumeSlider();
        MusicVolumeSlider();
        EffectsVolumeSlider();
    }




    //Updates the slider and Input depending on which was used
    public void MasterVolumeSlider()
    {   CheckVolumeFloor(masterSlider.value, out float newVolFloat);

        //Internal volume is divided by 11, since the slider goes from ~0 - 11
        float newInternalVol = (newVolFloat / 11);
        float newUserVol = newVolFloat;

        masterInput.text = (newUserVol).ToString("F0");
        masterMixer.SetFloat("Master", Mathf.Log10(newInternalVol) * 20);
    }

    public void MasterVolumeInput()
    {   //Make internal and external numbers so it's easier for humans to read 
        string newUserVolString = string.Format("{0:##.#}", masterInput.text);
        bool canParse = float.TryParse(newUserVolString, out float newVolFloat);


        //If volume is 0 it'll reset to full volume, so we're just gonna set a floor for it
        CheckVolumeFloor(newVolFloat, out float newVolFloatSani);
        float newUserVol = newVolFloatSani;
        float newInternalVol = (newVolFloatSani / 11);



        if (canParse)
        {   masterSlider.value = newUserVol;
            MasterVolumeSlider();
        }
    }
    
    
    //Same as previous classes, but for Music instead of Master
    public void MusicVolumeSlider()
    {   CheckVolumeFloor(musicSlider.value, out float newVolFloat);

        float newInternalVol = (newVolFloat / 11);
        float newUserVol = Mathf.Round(newVolFloat);

        musicInput.text = (newUserVol).ToString("F0");
        masterMixer.SetFloat("Music", Mathf.Log10(newInternalVol) * 20);
    }

    public void MusicVolumeInput()
    {   //Make internal and external numbers so it's easier for humans to read 
        string newUserVolString = string.Format("{0:##.#}", musicInput.text);
        bool canParse = float.TryParse(newUserVolString, out float newVolFloat);


        //If volume is 0 it'll reset to full volume, so we're just gonna set a floor for it
        CheckVolumeFloor(newVolFloat, out newVolFloat);
        float newUserVol = newVolFloat;
        float newInternalVol = (newVolFloat / 11);



        if (canParse)
        {   musicSlider.value = newUserVol;
            MusicVolumeSlider();
        }
    }
    
    

    //Same as previous classes, but for Effects instead of Master
    public void EffectsVolumeSlider()
    {   CheckVolumeFloor(effectsSlider.value, out float newVolFloat);

        float newInternalVol = (newVolFloat / 11);
        float newUserVol = Mathf.Round(newVolFloat);

        effectsInput.text = (newUserVol).ToString("F0");
        masterMixer.SetFloat("Effects", Mathf.Log10(newInternalVol) * 20);
    }

    public void EffectsVolumeInput()
    {   
        string newUserVolString = string.Format("{0:##.#}", effectsInput.text);
        bool canParse = float.TryParse(newUserVolString, out float newVolFloat);


        //If volume is 0 it'll reset to full volume, so we're just gonna set a floor for it
        CheckVolumeFloor(newVolFloat, out newVolFloat);

        //Make internal and external numbers so it's easier for humans to read 
        float newUserVol = newVolFloat;
        float newInternalVol = (newVolFloat / 11);



        if (canParse)
        {   effectsSlider.value = newUserVol;
            EffectsVolumeSlider();
        }
    }
    
    



    private float CheckVolumeFloor(float newVolume, out float sanitizedOutput)
    {
        if(newVolume <= minimumSoundInput)
            return sanitizedOutput = minimumSoundInput;

        else
            return sanitizedOutput = newVolume;

    }
}
