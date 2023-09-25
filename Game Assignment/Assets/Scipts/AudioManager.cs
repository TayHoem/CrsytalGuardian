using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public UIManager ui;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public AudioMixer mixer;
    private void Start()
    {
        PlayMusic("Theme");

        // Load music and SFX volume settings from PlayerPrefs
        float musicVolume = PlayerPrefs.GetFloat("VolumeMusic", 1.0f); // Default to 1.0 if not found
        float sfxVolume = PlayerPrefs.GetFloat("VolumeSFX", 1.0f); // Default to 1.0 if not found

        // Apply the loaded volume settings
        MusicVolume(musicVolume);
        SFXVolume(sfxVolume);

        //// Check if 'ui' is assigned before accessing its fields
        if (ui != null)
        {
            // Update the UI sliders with the loaded volume values
            ui._musicSlider.value = musicVolume * 100.0f; // Convert to percentage
            ui._sfxSlider.value = sfxVolume * 100.0f; // Convert to percentage
        }
    }

    private void Update()
    {
        mixer.SetFloat("VolumeMusic", musicSource.volume);
        PlayerPrefs.SetFloat("VolumeMusic", musicSource.volume);
        mixer.SetFloat("VolumeSFX", sfxSource.volume);
        PlayerPrefs.SetFloat("VolumeSFX", sfxSource.volume);
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX()
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        // Ensure the input value is within the 0-100 range
        float volumePercentage = Mathf.Clamp(volume, 0.0f, 100.0f);

        // Convert the volumePercentage to the 0-1 range
        volume = volumePercentage / 100.0f;
        musicSource.volume = volume;

        // Save the music volume to PlayerPrefs
        PlayerPrefs.SetFloat("VolumeMusic", volume);
        PlayerPrefs.Save(); // Explicitly save PlayerPrefs data
    }

    public void SFXVolume(float volume)
    {
        // Ensure the input value is within the 0-100 range
        float volumePercentage = Mathf.Clamp(volume, 0.0f, 100.0f);

        // Convert the volumePercentage to the 0-1 range
        volume = volumePercentage / 100.0f;
        sfxSource.volume = volume;

        // Save the SFX volume to PlayerPrefs
        PlayerPrefs.SetFloat("VolumeSFX", volume);
        PlayerPrefs.Save(); // Explicitly save PlayerPrefs data
    }
}
