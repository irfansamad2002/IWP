using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;

public class SetTutorialInfo : MonoBehaviour
{
   
    public enum VideoTutSelector
    {
        FreezeOrbTutVid,
        RewindTutVid
    }
    [Header("UI")]
    [SerializeField] TMP_Text headerText;
    [SerializeField] TMP_Text bodyText;
    [SerializeField] List<VideoClip> videoClips = new List<VideoClip>();

    [Space(10)]
    [SerializeField] Volume postProcessingVolume;

    [Space(10)]
    public GameObject playerGo;

    [Space(10)]
    public VideoPlayer videoPlayer;

    private VideoClip currentSelectedVideo;
    float videoDuration;

    private void OnEnable()
    {
        InputReader.OnSkipEvent += InputReader_OnSkipEvent;
    }

    private void OnDisable()
    {
        InputReader.OnSkipEvent -= InputReader_OnSkipEvent;
    }


    public void Init(string headerText, string bodyText, VideoTutSelector selector)
    {
        this.headerText.text = headerText;
        this.bodyText.text = bodyText;
        currentSelectedVideo = videoClips[(int)selector];
        videoDuration = (float)videoClips[(int)selector].length;
        videoPlayer.clip = currentSelectedVideo;
    }

   

    private void InputReader_OnSkipEvent()
    {
        CheckIfPlayerSkipped();
    }


    public void Activate()
    {
        gameObject.SetActive(true);
        MakeBackgroundBlur();
        PreventPlayerControls();
        PlayTheVideo();
        Debug.Log(videoDuration);
        //CancelInvoke();
        StopCoroutine(BackToNormalRoutine(videoDuration));
        StartCoroutine(BackToNormalRoutine(videoDuration));
        //Invoke(nameof(BackToNormal), videoDuration);

    }
    [ContextMenu("blur")]
    private void MakeBackgroundBlur()
    {
        postProcessingVolume.profile.components[3].active = true;

    }
    [ContextMenu("PreventPlayerControls")]
    private void PreventPlayerControls()
    {
        playerGo.SetActive(false);
    }

    [ContextMenu("ResumePlayerControls")]
    private void ResumePlayerControls()
    {
        playerGo.SetActive(true);
    }

    private void CheckIfPlayerSkipped()
    {
        UnBlur();
        ResumePlayerControls();
        gameObject.SetActive(false);
    }
    private void PlayTheVideo()
    {
        videoPlayer.Play();
    }

    private void CloseTheTutorialGameObject()
    {
        this.gameObject.SetActive(false);
    }

    private void BackToNormal()
    {
        CloseTheTutorialGameObject();
        ResumePlayerControls();
        UnBlur();
    }

    IEnumerator BackToNormalRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        CloseTheTutorialGameObject();
        ResumePlayerControls();
        UnBlur();
    }

    [ContextMenu("UnBlur")]
    private void UnBlur()
    {
        postProcessingVolume.profile.components[3].active = false;

    }
}
