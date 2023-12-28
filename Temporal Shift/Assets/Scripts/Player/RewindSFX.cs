using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindSFX : MonoBehaviour
{
    bool rewindingEvent = false;
    bool stopRewindingEvent = false;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip rewindSFX;
    private void OnEnable()
    {
        TimeRewindController.OnRewindingObjectUI += TimeRewindController_OnShowCancelUI;
        TimeRewindController.OnNotLookingAtRewindObjectUI += TimeRewindController_OnHideUI;
    }

   

    private void OnDisable()
    {
        TimeRewindController.OnRewindingObjectUI -= TimeRewindController_OnShowCancelUI;
        TimeRewindController.OnNotLookingAtRewindObjectUI -= TimeRewindController_OnHideUI;
    }


    private void TimeRewindController_OnHideUI()
    {
        if (!stopRewindingEvent)
        {
            //Debug.Log("stop playing audio");

            audioSource.Stop();

            rewindingEvent = false;
            stopRewindingEvent = true;
        }
    }

    private void TimeRewindController_OnShowCancelUI()
    {
        if (!rewindingEvent)
        {
            //Debug.Log("play audio");

            audioSource.clip = rewindSFX;
            audioSource.Play();

            rewindingEvent = true;
            stopRewindingEvent = false;
        }
    }

    



}
