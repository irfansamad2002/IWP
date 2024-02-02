using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateByMultipleButtonsEvent : MonoBehaviour
{
    public event Action OnActivation;
    public List<MultiButtonTrigger> allTheButtonsRequired = new List<MultiButtonTrigger>();

    [SerializeField] float activationDuration = 2f;
    

    // Update is called once per frame
    void Update()
    {
        if (IfAllButtonPressed())
        {
            //Debug.Log("All pressed");
            OnActivation?.Invoke();
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

   


   
}
