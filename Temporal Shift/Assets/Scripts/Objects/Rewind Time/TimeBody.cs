using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TimeBody : MonoBehaviour, IRewindTimeable
{
    public event Action RewindingTime;
    public bool isRewinding = false;

    public float recordTime = 5f;

    List<PointInTime> pointsInTime;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
    }

    private void FixedUpdate()
    {
        

        if (isRewinding)
        {
            RewindingTime?.Invoke();
            Rewind();
        }
        else
        {

            Record();
        }
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
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
        isRewinding = true;
        if (rb)
            rb.isKinematic = true;
    }

    public void StopRewinding()
    {
        isRewinding = false;
        if (rb)
            rb.isKinematic = false;
    }
}
