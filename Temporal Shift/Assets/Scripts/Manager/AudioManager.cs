using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    public static AudioManager Instance;

    public const string MASTER_KEY = "masterVolumeKey";
    public const string AMBIENCE_KEY = "ambienceVolumeKey";
    public const string SFX_KEY = "sfxVolumeKey";

   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        LoadVolume();
    }

    void LoadVolume() // Volume saved in VolumeSetting.cs
    {
        float masterVol = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float ambienceVol = PlayerPrefs.GetFloat(AMBIENCE_KEY, 1f);
        float sfxVol = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSetting.MIXER_MASTER, Mathf.Log10(masterVol) * 20);
        mixer.SetFloat(VolumeSetting.MIXER_AMBIENCE, Mathf.Log10(ambienceVol) * 20);
        mixer.SetFloat(VolumeSetting.MIXER_SFX, Mathf.Log10(sfxVol) * 20);
    }

}
