using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateByMultipleButtons : MonoBehaviour
{
    public event Action<float> OnActivation;


    public List<MultiButtonTrigger> allTheButtonsRequired = new List<MultiButtonTrigger>();
    Animator _anim;

    [SerializeField] float HowLongDoorOpen = 4f;



    private void OnEnable()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IfAllButtonPressed())
        {
            //Debug.Log("All pressed");
            OnActivation?.Invoke(HowLongDoorOpen);
            Activate(HowLongDoorOpen);
        }
    }

    private bool IfAllButtonPressed()
    {
        bool allButtonPressed = true;

        foreach (MultiButtonTrigger button in allTheButtonsRequired)
        {
            if (!button.IsPressed())
            {
                allButtonPressed = false;
                break;
            }


        }

        return allButtonPressed;
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
