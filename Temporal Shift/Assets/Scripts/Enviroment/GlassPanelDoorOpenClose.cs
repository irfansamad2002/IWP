using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlassPanelDoorOpenClose : MonoBehaviour, IInteractable
{
    [SerializeField] bool isOpen = false;

    float targetYAxis;

    float currentYAxis;

    [SerializeField] float lerpSpeed = 2f;
    [SerializeField] float openDoorEuler = 30;
    [SerializeField] float closeDoorEuler =-90f;

    [SerializeField] TMP_Text textUI;

    private void Start()
    {
        currentYAxis = closeDoorEuler;
        targetYAxis = closeDoorEuler;

    }
    public void Interacted()
    {

        OpenOrCloseDoor();
        isOpen = !isOpen;
    }

    void OpenOrCloseDoor()
    {
        //if its already open, close the door
        if (isOpen)
        {
            CloseDoor();
            textUI.text = "Press E To Open Door";

        }
        //if its closed, open the door
        else
        {
            OpenDoor();
            textUI.text = "Press E To Close Door";
        }
    }

    private void OpenDoor()
    {
        targetYAxis = openDoorEuler;
    }

    private void CloseDoor()
    {
        targetYAxis = closeDoorEuler;
    }

    private void Update()
    {
        currentYAxis = Mathf.Lerp(currentYAxis, targetYAxis, lerpSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, currentYAxis, 0);
    }


}
