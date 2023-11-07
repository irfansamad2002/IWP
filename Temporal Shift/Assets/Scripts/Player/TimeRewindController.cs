using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindController : MonoBehaviour
{
    //public float maxDistance = 10f; // Maximum distance for rewinding
    //private InputReader inputReader;
    //private bool rewindThatObject = false;

    //private List<GameObject> rewindedObjects = new List<GameObject>();

    //private bool test = false;
    //private void OnEnable()
    //{
    //    inputReader.RewindEvent += HandleRewindInput;
    //}



    //private void OnDisable()
    //{
    //    inputReader.RewindEvent -= HandleRewindInput;

    //}

    //private void Awake()
    //{
    //    inputReader = GetComponent<Movement>().inputReader;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //CheckObjectToRewind();
    //    if(!rewindThatObject)
    //        MakeEveryObjectMoveAtFirst();
    //}

    //private void LateUpdate()
    //{

    //    CheckObjectToRewind();

    //}

    //private void HandleRewindInput(bool state)
    //{
    //    rewindThatObject = state;
    //}

    //private void MakeEveryObjectMoveAtFirst()
    //{

    //    if (rewindedObjects.Count == 0)
    //        return;

    //    // Notify the objects inside the orb to resume their movement
    //    foreach (var rewindedObject in rewindedObjects)
    //    {
    //        IRewindTimeable iRewindTimeable = rewindedObject.GetComponent<IRewindTimeable>();
    //        if (iRewindTimeable != null)
    //        {
    //            iRewindTimeable.StopRewinding();
    //        }


    //        // Check if the player is currently looking at the object
    //        if (!IsPlayerLookingAtObject(rewindedObject))
    //        {

    //            rewindedObjects.Remove(rewindedObject);

    //        }
    //    }
    //}

    //private void CheckObjectToRewind()
    //{
    //    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, maxDistance))
    //    {
    //        IRewindTimeable iRewindTimeable = hit.collider.gameObject.GetComponent<IRewindTimeable>();
    //        if (iRewindTimeable != null)
    //        {
    //            if (rewindThatObject)
    //            {
    //                iRewindTimeable.StartRewinding();
    //                if(!rewindedObjects.Contains(hit.collider.gameObject))
    //                    rewindedObjects.Add(hit.collider.gameObject);
    //            }
    //            else
    //            {
    //                iRewindTimeable.StopRewinding();
    //                rewindedObjects.Remove(hit.collider.gameObject);

    //            }
    //        }






    //    }
    //}

    //private bool IsPlayerLookingAtObject(GameObject obj)
    //{
    //    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, maxDistance))
    //    {
    //        return hit.collider.gameObject == obj;
    //    }

    //    return false;
    //}

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
