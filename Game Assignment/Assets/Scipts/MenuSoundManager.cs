using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    [Header("---------Audio Source-----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------Audio Clip-----------")]
    public AudioClip shoot;
    public AudioClip levelUp;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
