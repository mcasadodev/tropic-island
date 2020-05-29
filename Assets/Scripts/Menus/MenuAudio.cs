using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuAudio : MonoBehaviour
{
    public AudioMixer masterMixer, musicMixer, sfxMixer;
    //public Slider masterSlider, musicSlider, sfxSlider;

    public void SetMasterVolume(float sliderValue)
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue)
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }
}
