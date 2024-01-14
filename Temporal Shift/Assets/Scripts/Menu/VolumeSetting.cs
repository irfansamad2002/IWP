using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    [SerializeField] Slider masterSlider, ambienceSlider, sfxSlider;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_AMBIENCE = "AmbienceVolume";
    public const string MIXER_SFX = "SFXVolume";

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.AMBIENCE_KEY, ambienceSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }



    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        ambienceSlider.onValueChanged.AddListener(SetAmbienceVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        ambienceSlider.value = PlayerPrefs.GetFloat(AudioManager.AMBIENCE_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }

    #region audio Mixer

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
    }

    void SetAmbienceVolume(float value)
    {
        mixer.SetFloat(MIXER_AMBIENCE, Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    #endregion
   
}
