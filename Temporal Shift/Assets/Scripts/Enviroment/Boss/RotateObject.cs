using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour, IRewindTimeable, IStopTimeable
{
    public float RotateSpeed = 35f;

    private int direction = 1;

    private bool isRewinding;
    private float orignalRotateSpeed;

    [SerializeField] TimeRewindController controller;
    public static event Action<RotateObject> OnRewindStarted;
    public static event Action<RotateObject> OnRewindStopped;


    private void Start()
    {
        orignalRotateSpeed = RotateSpeed;
    }

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, RotateSpeed * direction * Time.deltaTime); 
    }

    public void StartRewinding()
    {
        if (!isRewinding)
        {
            direction = -direction;
            isRewinding = true;

            OnRewindStarted?.Invoke(this);

        }

    }

    public void StopRewinding()
    {
        Debug.Log("Stop");
        direction = -direction;
        isRewinding = false;

        OnRewindStopped?.Invoke(this);

    }

    public void AbleToRewind()
    {

    }

    public void StopMoving()
    {
        RotateSpeed = 0;
    }

    public void StartMoving()
    {
        RotateSpeed = orignalRotateSpeed;
    }
}
