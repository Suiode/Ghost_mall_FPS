using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsScript : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;



    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        else
        {
            PlayerPrefs.SetFloat("MasterVolume", 10f);
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }

        if (!PlayerPrefs.HasKey("MusicVolume"))
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 10f);
            masterSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (!PlayerPrefs.HasKey("EffectsVolume"))
            effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        else
        {
            PlayerPrefs.SetFloat("EffectsVolume", 10f);
            masterSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetEffectsVolume()
    {
        PlayerPrefs.SetFloat("EffectsVolume", effectsSlider.value);
    }


}
