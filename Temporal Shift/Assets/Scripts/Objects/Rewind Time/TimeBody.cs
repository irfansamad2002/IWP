using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TimeBody : MonoBehaviour, IRewindTimeable
{
    public event Action OnRewinding;
    public event Action OnStopRewinding;
    public bool isRewinding = false;
    public float recordTime = 5f;


    List<PointInTime> pointsInTime;
    OutlineColors outlineColors;
    Outline outline;
    Rigidbody rb;
    bool lookedAtOrRewinding;

    [Header("For External Motion")]
    public Vector3 DirectionToPreviousState = Vector3.zero;
    LoopWaypointsObjectMovement platformMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        platformMovement = GetComponent<LoopWaypointsObjectMovement>();
        outlineColors = GetComponent<OutlineColors>();
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
    }

    private void FixedUpdate()
    {
        outline.OutlineColor = outlineColors.NotLookingColor;


        if (isRewinding)
        {
            if(platformMovement != null)
                platformMovement.DirectionToWaypoint = DirectionToPreviousState;

            Rewind();
        }
        else
        {
            Record();
        }

        //Debug.Log(platformMovement.DirectionToWaypoint);
        //Debug.Log("DirectionToPreviousState " + DirectionToPreviousState);
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            DirectionToPreviousState = pointInTime.position - transform.position;
            DirectionToPreviousState.Normalize();
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewinding();
        }
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewinding()
    {
        outline.OutlineColor = outlineColors.RewindingObjectColor;
        isRewinding = true;
        if (rb)
            rb.isKinematic = true;
    }

    public void StopRewinding()
    {
        outline.OutlineColor = outlineColors.NotLookingColor;

        DirectionToPreviousState = Vector3.zero;
        isRewinding = false;
        if (rb)
            rb.isKinematic = false;
    }

    public void AbleToRewind()
    {
        if (!isRewinding)
        {
            outline.OutlineColor = outlineColors.AbleToRewindObjectColor;

        }
    }
}
