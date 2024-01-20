using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ShowTutorialVideoPlayer : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject screenGameObject;
    [SerializeField] GameObject playerGameObject;
    [SerializeField] HideMouseOnFocus hideMouse;

    [SerializeField] List<VideoClip> videoClipsForTutorial = new List<VideoClip>();

    private float timeMultiplier = 1f;
    public int videoIndex = 0;
    public string instruction = "Forgot to set lmao";

    [ContextMenu("PlayTheVideo")]
    public void PlayVideo()
    {
        hideMouse.ShowCursor();

        //Stop player from looking around
        playerGameObject.GetComponent<LookAroundWithMouse>().enabled = false;
        screenGameObject.GetComponent<SetTutorialInfo>().TutorialText.text = instruction;
        //screenGameObject.GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text = "Skip";
        screenGameObject.SetActive(true);
        timeMultiplier = 0f;
        videoPlayer.clip = videoClipsForTutorial[videoIndex];
        videoPlayer.Play();
        Utils.RunAfterSecondsRealtime(this, (float)videoPlayer.clip.length, StopShowingTutorial);
       

    }

    [ContextMenu("StopTheVideo")]
    public void StopShowingTutorial()
    {
        hideMouse.HideCursor();


        timeMultiplier = 1f;
        playerGameObject.GetComponent<LookAroundWithMouse>().enabled = true;
        screenGameObject.SetActive(false);

    }



    // Update is called once per frame
    void Update()
    {
        
        Time.timeScale = timeMultiplier;
    }
}
