using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour, IRewindTimeable
{
    public float RotateSpeed = 35f;

    private int direction = 1;

    private bool isRewinding;

    [SerializeField] TimeRewindController controller;
    public static event Action<RotateObject> OnRewindStarted;
    public static event Action<RotateObject> OnRewindStopped;

    // Update is called once per frame
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

  
}
