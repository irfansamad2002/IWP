using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindController : MonoBehaviour
{
    public static event Action OnShowCancelUI;
    public static event Action OnShowHoldUI;
    public static event Action OnHideUI;

    [SerializeField] private LayerMask layerMask;
    public float maxDistance = 10f; // Maximum distance for rewinding
    private InputReader inputReader;
    private bool isHoldingRightClick = false;
    private bool isLookingObject = false;
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

        if (isLookingObject)
        {
            //show ui
            if (isHoldingRightClick)
            {
                OnShowCancelUI?.Invoke();
            }
            else
            {
                OnShowHoldUI?.Invoke();
            }
        }
        else
        {
            if (isHoldingRightClick && rewindingObject != null)
            {
                OnShowCancelUI?.Invoke();

            }
            else
            {
                OnHideUI?.Invoke();
            }
                
        }

    }

    private void CheckObjectToRewind()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, maxDistance, layerMask))
        {
            IRewindTimeable iRewindTimeable = hit.collider.gameObject.GetComponent<IRewindTimeable>();
            if (iRewindTimeable != null)
            {
                //OnAbleToRewind?.Invoke();
                isLookingObject = true;
                if (isHoldingRightClick)
                {
                    rewindingObject = hit.collider.gameObject; // Set the object to rewind
                }
            }
            else
            {
                isLookingObject = false;

            }
        }
        else
        {
            isLookingObject = false;

        }



    }

}
