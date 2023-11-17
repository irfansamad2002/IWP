using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowUITextBaseOnTrigger : MonoBehaviour
{
    [SerializeField] TMP_Text textUI;
    private void Start()
    {
        textUI.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        textUI.enabled = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        textUI.enabled = false;


    }
}
