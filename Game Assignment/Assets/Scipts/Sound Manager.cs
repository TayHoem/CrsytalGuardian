using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource _musicSource, _effectSource;
    [SerializeField] private Slider volumeSlider;

    private float minVolume = 0.0f; // Minimum volume (0%)
    private float maxVolume = 100.0f; // Maximum volume (100%)

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", maxVolume);
            PlayerPrefs.Save(); // Save the default volume
        }
        Load();
        ChangeVolume(); // Apply the loaded volume
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ChangeVolume()
    {
        float scaledVolume = volumeSlider.value / maxVolume; // Scale to 0-1 range
        AudioListener.volume = scaledVolume;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        Debug.Log("Loaded musicVolume: " + volumeSlider.value);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.Save(); // Explicitly save PlayerPrefs data
        Debug.Log("Saved musicVolume: " + volumeSlider.value);
    }

    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        float scaledVolume = value / maxVolume; // Scale to 0-1 range
        AudioListener.volume = scaledVolume;
    }

    public void ToggleEffects()
    {
        _effectSource.mute = !_effectSource.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
}
