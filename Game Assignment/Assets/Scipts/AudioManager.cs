using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

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
    private void Start()
    {
        PlayMusic("Theme");
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
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

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
    public float MusicVolume(float volume)
    {
        // Ensure the input value is within the 0-100 range
        float volumePercentage = Mathf.Clamp(volume, 0.0f, 100.0f);

        // Convert the volumePercentage to the 0-1 range
        volume = volumePercentage/100;
        musicSource.volume = volume;
        return musicSource.volume;
    }

    public float SFXVolume(float volume)
    {
        // Ensure the input value is within the 0-100 range
        float volumePercentage = Mathf.Clamp(volume, 0.0f, 100.0f);

        // Convert the volumePercentage to the 0-1 range
        volume = volumePercentage/100;
        sfxSource.volume = volume;
        return sfxSource.volume;
    }
}
