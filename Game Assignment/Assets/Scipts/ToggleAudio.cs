using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toogleMusic, _toogleEffects;
    // Start is called before the first frame update
    public void Toggle()
    {
        if (_toogleEffects)SoundManager.Instance.ToggleEffects();
        if (_toogleMusic) SoundManager.Instance.ToggleMusic();
    }
}
