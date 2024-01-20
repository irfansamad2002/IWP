using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTutorialTrigger : MonoBehaviour
{
    [SerializeField] ShowTutorialVideoPlayer showTut;
    private bool shown;

    public int videoIndex = 0;
    public string instructionText = "Forgot to set lmao";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!shown)
            {
                showTut.videoIndex = videoIndex;
                showTut.instruction = instructionText;
                showTut.PlayVideo();
                shown = true;
            }
            


        }

    }
    

}
