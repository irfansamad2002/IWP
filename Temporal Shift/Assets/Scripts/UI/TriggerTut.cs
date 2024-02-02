using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerTut : MonoBehaviour
{
    [SerializeField] SetTutorialInfo setTutorialInfo;

    public string headerText, bodyText;
    //public text
    public SetTutorialInfo.VideoTutSelector selector;
    private bool playOnce;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || playOnce)
            return;
        setTutorialInfo.Init(headerText, bodyText, selector);
        setTutorialInfo.Activate();
        playOnce = true;
    }
}
