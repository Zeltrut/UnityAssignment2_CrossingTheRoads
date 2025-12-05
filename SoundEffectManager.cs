using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoundEffectManager : MonoBehaviour
{
    [Header("Master Volume")]
    [SerializeField] private Slider masterVolumeSlider;

    [Header("Audio Sources Parent")]
    [SerializeField] private List<Transform> audioSourceParents;

    // Static property so any script can access the current volume setting
    public static float GlobalVolume { get; private set; } = 1.0f;

    private List<AudioSource> allAudioSources;
    private const string MASTER_VOLUME_PREFS_KEY = "MasterVolume";

    void Awake()
    {
        allAudioSources = new List<AudioSource>();

        if (audioSourceParents != null)
        {
            foreach (Transform parent in audioSourceParents)
            {
                if (parent != null)
                {
                    allAudioSources.AddRange(parent.GetComponentsInChildren<AudioSource>(true));
                }
            }
        }
        
        if (masterVolumeSlider != null)
        {
            // Load saved volume
            GlobalVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_PREFS_KEY, 1.0f);
            
            masterVolumeSlider.SetValueWithoutNotify(GlobalVolume);
            SetMasterVolume(GlobalVolume);

            masterVolumeSlider.onValueChanged.AddListener(OnMasterSliderValueChanged);
        }
    }
    
    private void OnMasterSliderValueChanged(float value)
    {
        GlobalVolume = value;
        SetMasterVolume(value);
        PlayerPrefs.SetFloat(MASTER_VOLUME_PREFS_KEY, value);
    }

    public void SetMasterVolume(float volume)
    {
        // Update all registered audio sources
        foreach (AudioSource source in allAudioSources)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }
    }
}