using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;
    public delegate void ExperienceChangeHandler(float amount);
    public event ExperienceChangeHandler OnExperienceChange;

    //Singleton check
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddExperince(float amount)
    {
        OnExperienceChange?.Invoke(amount);
    }
}
