using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class RotateWall : MonoBehaviour, IStopTimeable, IRewindTimeable
{
    [SerializeField] Vector3 rotationAxis = Vector3.up;
    [SerializeField] Transform rotationPoint;
    [SerializeField] private float rotationSpeed = 2f; // Adjust the speed in the Inspector

    float originalSpeed;

    private void Start()
    {
        originalSpeed = rotationSpeed;
    }

    public void AbleToRewind()
    {

    }

    public void StartMoving()
    {
        rotationSpeed = originalSpeed;
    }

    public void StartRewinding()
    {
        float negSpeed = Math.Abs(rotationSpeed);
        rotationSpeed = -negSpeed;
    }

    public void StopMoving()
    {
        rotationSpeed = 0;
    }

    public void StopRewinding()
    {
        rotationSpeed = originalSpeed;
    }

    private void Update()
    {
        transform.RotateAround(rotationPoint.position, rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
