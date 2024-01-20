using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParent : MonoBehaviour
{

    private RotateObject currentRewindingObject;

    private void OnEnable()
    {
        RotateObject.OnRewindStarted += RotateObject_OnRewindStarted;
        RotateObject.OnRewindStopped += RotateObject_OnRewindStopped;
    }

   

    private void OnDisable()
    {
        RotateObject.OnRewindStarted -= RotateObject_OnRewindStarted;
        RotateObject.OnRewindStopped -= RotateObject_OnRewindStopped;
    }


    private void RotateObject_OnRewindStarted(RotateObject rotatingObj)
    {
        //nother object is already rewinding, stop it
        if (currentRewindingObject != null && currentRewindingObject != rotatingObj)
        {
            currentRewindingObject.StopRewinding();
        }

        // Set the current rewinding object
        currentRewindingObject = rotatingObj;
    }

    private void RotateObject_OnRewindStopped(RotateObject rotatingObj)
    {
        // Reset the current rewinding object
        if (currentRewindingObject == rotatingObj)
        {
            currentRewindingObject = null;
        }
    }

}
