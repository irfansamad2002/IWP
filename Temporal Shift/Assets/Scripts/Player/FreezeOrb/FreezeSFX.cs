using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip startFreezeSFX;
    [SerializeField] AudioClip stoppedOrbMoveSFX;
    [SerializeField] AudioClip destroyOrbSFX;
    private void OnEnable()
    {
        FreezeOrb.OnStopOrb += FreezeOrb_OnStopOrb;
        FreezeOrb.OnDestroyOrb += FreezeOrb_OnDestroyOrb;
    }

   
    private void OnDisable()
    {
        FreezeOrb.OnStopOrb -= FreezeOrb_OnStopOrb;
        FreezeOrb.OnDestroyOrb -= FreezeOrb_OnDestroyOrb;

    }

    private void Awake()
    {
        //Debug.Log("play Awake audio");
        audioSource.PlayOneShot(startFreezeSFX);

    }

    


    private void FreezeOrb_OnStopOrb()
    {
        //Debug.Log("play FreezeOrb_OnStopOrb audio");
        audioSource.PlayOneShot(stoppedOrbMoveSFX);
    }

    private void FreezeOrb_OnDestroyOrb()
    {
        //Debug.Log("play FreezeOrb_OnDestroyOrb audio");

        // Get the length of the audio clip
        float audioClipLength = destroyOrbSFX.length;
        Destroy(audioSource.gameObject, audioClipLength);

        audioSource.gameObject.transform.parent = null;
        audioSource.PlayOneShot(destroyOrbSFX);


    }

   

}
