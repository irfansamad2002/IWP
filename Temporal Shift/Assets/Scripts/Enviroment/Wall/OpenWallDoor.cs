using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWallDoor : MonoBehaviour
{
    [SerializeField] float HowLongDoorOpen = 4f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip OpenDoor, CloseDoor;

    ActivateByMultipleButtonsEvent activateByMultipleButtonsNew;
    Animator _anim;

    bool openedDoor;
    bool checkedOpen;
    bool checkedClose;
    private void Awake()
    {
        activateByMultipleButtonsNew = GetComponent<ActivateByMultipleButtonsEvent>();

    }

    private void OnEnable()
    {
        _anim = GetComponent<Animator>();
        if(activateByMultipleButtonsNew)
            activateByMultipleButtonsNew.OnActivation += ActivateByMultipleButtonsNew_OnActivation;
    }

    private void OnDisable()
    {
        if(activateByMultipleButtonsNew)
            activateByMultipleButtonsNew.OnActivation -= ActivateByMultipleButtonsNew_OnActivation;

    }

    private void ActivateByMultipleButtonsNew_OnActivation()
    {
        Activate(HowLongDoorOpen);
    }

    #region ActivateOpenDoor
    public void Activate(float howLong)
    {
        StartCoroutine(OpenDoorForSomeTime(howLong));
       
    }

    IEnumerator OpenDoorForSomeTime(float someTime)
    {

        _anim.Play("WallOpen");
        openedDoor = true;
        yield return new WaitForSeconds(someTime);
        _anim.Play("WallClose");
        openedDoor = false;

    }

    #endregion

    private void Update()
    {
        if (openedDoor && !checkedOpen)
        {
            audioSource.PlayOneShot(OpenDoor);
            checkedOpen = true;
            checkedClose = false;
        }
        if (!openedDoor && !checkedClose)
        {
            audioSource.PlayOneShot(CloseDoor);
            checkedClose = true;
            checkedOpen = false;
        }
    }

    public void PlayOpenDoor()
    {
        audioSource.PlayOneShot(OpenDoor);
    }

    public void PlayCloseDoor()
    {
        audioSource.PlayOneShot(CloseDoor);

    }
}
