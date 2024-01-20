using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWallDoor : MonoBehaviour
{
    Animator _anim;

    [SerializeField] float HowLongDoorOpen = 4f;
    ActivateByMultipleButtonsEvent activateByMultipleButtonsNew;

    private void Awake()
    {
        activateByMultipleButtonsNew = GetComponent<ActivateByMultipleButtonsEvent>();

    }

    private void OnEnable()
    {
        _anim = GetComponent<Animator>();
        activateByMultipleButtonsNew.OnActivation += ActivateByMultipleButtonsNew_OnActivation;
    }

    private void OnDisable()
    {
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
        yield return new WaitForSeconds(someTime);
        _anim.Play("WallClose");
    }

    #endregion

   
}
