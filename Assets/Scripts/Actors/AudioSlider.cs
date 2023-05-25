using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    void Start() {
        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("sfxVolume")) LoadVolume();
        else {
            SetSFXVolume();
            SetMusicVolume();
        }
    }

    public void SetSFXVolume() {
        float volume = sfxSlider.value;
        mixer.SetFloat("sfx",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume",volume);
    }

    public void SetMusicVolume() {
        float volume = musicSlider.value;
        mixer.SetFloat("music",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume",volume);
    }

    public void LoadVolume() {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SetSFXVolume();
        SetMusicVolume();
    }
}
