using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MasterVolumeControl : MonoBehaviour
{
    [Header("UI References")]
    public Slider volumeSlider;

    [Header("Audio Mixer Reference")]
    public AudioMixer audioMixer;

    // Name of the exposed parameter in the AudioMixer
    [SerializeField] private string masterParameter = "Master";

    private void Start()
    {
        // Load saved volume if exists
        if (PlayerPrefs.HasKey(masterParameter))
        {
            float savedVolume = PlayerPrefs.GetFloat(masterParameter);
            volumeSlider.value = savedVolume;
            SetMasterVolume(savedVolume);
        }
        else
        {
            // Set initial volume from slider default
            SetMasterVolume(volumeSlider.value);
        }

        // Listen to slider changes
        volumeSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    private void SetMasterVolume(float value)
    {
        // Convert slider value (0 to 1) → dB scale (-80dB to 0dB)
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(masterParameter, dB);

        // Save for persistence
        PlayerPrefs.SetFloat(masterParameter, value);
    }
}
