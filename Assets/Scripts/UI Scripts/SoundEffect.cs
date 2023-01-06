using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{

    [Header("Sound effects for click confirmation sound")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundClip;


    // Start is called before the first frame update
    public void PlaySound()
    {
        audioSource.clip = soundClip;
        audioSource.Play();
    }
}
