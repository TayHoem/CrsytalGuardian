using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public AudioMixer mixer;
    public float volumeMusicValues = 50f, volumeSFXValues = 50f;

    public void Start()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("VolumeMusic");
        _sfxSlider.value = PlayerPrefs.GetFloat("VolumeSFX");
    }
    public void Update()
    {
        mixer.SetFloat("VolumeMusic", volumeMusicValues);
        PlayerPrefs.SetFloat("VolumeMusic", volumeMusicValues * 100);
        mixer.SetFloat("VolumeSFX", volumeSFXValues);
        PlayerPrefs.SetFloat("VolumeSFX", volumeSFXValues * 100);
    }
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        volumeMusicValues = AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        volumeSFXValues = AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}
