using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    AudioManager audioManager;  
    [SerializeField] private GameObject mainMenuCanvas;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;


    void Start()
    {
        audioManager = AudioManager.Instance;
        
        musicSlider.onValueChanged.AddListener(HandleMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(HandleSFXVolumeChanged);

        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1);
        musicSlider.SetValueWithoutNotify(savedMusicVolume);
        sfxSlider.SetValueWithoutNotify(savedSFXVolume);
    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(HandleMusicVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(HandleSFXVolumeChanged);
    }

    private void HandleMusicVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        audioManager.SetMusicVolume(volume);
    }

    private void HandleSFXVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        audioManager.SetSFXVolume(volume);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }

    private void GoBack()
    {
        this.gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
