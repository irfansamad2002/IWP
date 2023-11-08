using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindController : MonoBehaviour
{
    public float maxDistance = 10f; // Maximum distance for rewinding
    private InputReader inputReader;
    private bool isHoldingRightClick = false;
    private GameObject rewindingObject;

    private void OnEnable()
    {
        inputReader.RewindEvent += HandleRewindInput;
    }

    private void OnDisable()
    {
        inputReader.RewindEvent -= HandleRewindInput;
    }

    private void Awake()
    {
        inputReader = GetComponent<Movement>().inputReader;
    }

    private void HandleRewindInput(bool state)
    {
        isHoldingRightClick = state;
        if (!state && rewindingObject != null)
        {
            rewindingObject.GetComponent<Outline>().enabled = false;
            rewindingObject.GetComponent<IRewindTimeable>().StopRewinding(); // Stop rewinding when the player releases right-click
            rewindingObject = null;
        }
    }

    private void Update()
    {
        CheckObjectToRewind();
        
        if (isHoldingRightClick && rewindingObject != null)
        {
            rewindingObject.GetComponent<IRewindTimeable>().StartRewinding(); // Rewind time when holding right-click
        }

    }

    private void CheckObjectToRewind()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, maxDistance))
        {
            IRewindTimeable iRewindTimeable = hit.collider.gameObject.GetComponent<IRewindTimeable>();
            if (iRewindTimeable != null)
            {
               if (isHoldingRightClick)
               {
                    rewindingObject = hit.collider.gameObject; // Set the object to rewind
               }
            }

            
            
        }

    }

}
