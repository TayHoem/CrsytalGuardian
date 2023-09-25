using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResolutionControl : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolution;
    private List<Resolution> filterResolution;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        resolution = Screen.resolutions;
        filterResolution = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for(int i = 0; i < resolution.Length; i++)
        {
            if (resolution[i].refreshRate == currentRefreshRate)
            {
                filterResolution.Add(resolution[i]);
            }
        }
        List<string> options = new List<string>();
        for (int i = 0; i < filterResolution.Count; i++)
        {
            string resolutionOption = filterResolution[i].width + "x" + filterResolution[i].refreshRate + " Hz";
            options.Add(resolutionOption);
            if (filterResolution[i].width == Screen.width && filterResolution[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filterResolution[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,true);
    }
}
